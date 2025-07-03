using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using So2Baladna.API.Helper;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RateLimitingMiddleware> _logger;
    private readonly IMemoryCache _memoryCache;

    private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(10);
    private readonly int _maxRequests = 8;

    public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger, IMemoryCache memoryCache)
    {
        _next = next;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var cacheKey = $"Rate:{ip}";
        var dateNow = DateTime.UtcNow;

        var (timeStamp, count) = _memoryCache.GetOrCreate(cacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
            return (dateNow, 0);
        });

        if (dateNow - timeStamp < _rateLimitWindow)
        {
            if (count >= _maxRequests)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                context.Response.ContentType = "application/json";
                var response =  new ApiException((int)HttpStatusCode.TooManyRequests, "Rate limit exceeded. Please try again later.") ;
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
                
            }

            _memoryCache.Set(cacheKey, (timeStamp, count + 1), _rateLimitWindow);
        }
        else
        {
            _memoryCache.Set(cacheKey, (dateNow, 1), _rateLimitWindow);
        }

        await _next(context);
    }
}
