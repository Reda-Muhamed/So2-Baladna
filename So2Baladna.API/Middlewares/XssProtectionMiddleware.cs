namespace So2Baladna.API.Middlewares
{
    public class XssProtectionMiddleware
    {
        private readonly RequestDelegate next;

        public XssProtectionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";
            context.Response.Headers["Content-Security-Policy"] = "default-src 'self'; script-src 'self'";
            await next(context);

        }

    }
}
