namespace PowerCalculator.WebApi.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Items["X-Correlation-Id"] = Guid.NewGuid().ToString();

            await _next(httpContext);
        }
    }
}
