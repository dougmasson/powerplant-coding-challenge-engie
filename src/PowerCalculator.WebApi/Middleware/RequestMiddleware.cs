namespace PowerCalculator.WebApi.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var correlationId = httpContext.Items["X-Correlation-Id"];

            httpContext.Request.Headers.TryAdd("X-Correlation-Id", correlationId.ToString());

            await _next(httpContext);
        }
    }
}
