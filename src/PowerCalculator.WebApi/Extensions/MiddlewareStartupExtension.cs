using PowerCalculator.WebApi.Middleware;

namespace PowerCalculator.WebApi.Extensions
{
    /// <summary>
    /// Configuration of Middleware.
    /// </summary>
    public static class MiddlewareStartupExtension
    {
        /// <summary>
        /// Include custom Middlewares.
        /// </summary>
        /// <remarks>Order is most important!</remarks>
        /// <returns>WebApplication configured.</returns>
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseMiddleware<LoggerMiddleware>();

            app.UseMiddleware<RequestMiddleware>();
            app.UseMiddleware<ResponseMiddleware>();

            return app;
        }
    }
}
