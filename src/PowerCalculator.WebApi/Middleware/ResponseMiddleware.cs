namespace PowerCalculator.WebApi.Middleware
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                var correlationId = httpContext.Items["X-Correlation-Id"];

                if (correlationId is not null)
                {
                    httpContext.Response.Headers.TryAdd("X-Correlation-Id", correlationId.ToString());
                }

                return Task.CompletedTask;
            }, httpContext);

            await _next(httpContext);
        }
    }
}
