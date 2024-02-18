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
            var correlationId = httpContext.Items["x-correlation-id"];

            httpContext.Request.Headers.TryAdd("x-correlation-id", correlationId!.ToString());

            await _next(httpContext);
        }
    }
}
