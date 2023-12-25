using Serilog.Context;

namespace PowerCalculator.WebApi.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var correlationId = httpContext.Items["x-correlation-id"];

            using (LogContext.PushProperty("CorellationId", correlationId))
            {
                await _next(httpContext);
            }
        }
    }
}
