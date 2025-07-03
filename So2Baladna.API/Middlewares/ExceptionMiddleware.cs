using Microsoft.Extensions.Hosting;
using So2Baladna.API.Helper;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace So2Baladna.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment webHostEnvironment ;

        public ExceptionMiddleware(RequestDelegate next,Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            this.next = next;
            this.webHostEnvironment = hostingEnvironment;
        }
        public async Task InvokeAsync(HttpContext context) {
            try
            {
               await next(context);
                

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError ;
                context.Response.ContentType = "application/json" ;
                var response = webHostEnvironment.IsDevelopment() ? new ApiException((int)HttpStatusCode.InternalServerError, ex.StackTrace, null, ex.Message) : new ApiException((int)HttpStatusCode.InternalServerError, ex.StackTrace, null);
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        
        
        }

    }
}
