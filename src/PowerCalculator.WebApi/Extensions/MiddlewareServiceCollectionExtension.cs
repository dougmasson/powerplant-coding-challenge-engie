using PowerCalculator.WebApi.Middleware;

namespace PowerCalculator.WebApi.Extensions
{
    /// <summary>
    /// Configuratin of Middleware.
    /// </summary>
    public static class MiddlewareServiceCollectionExtension
    {
        /// <summary>
        /// Include custom Middlewares.
        /// </summary>
        /// <remarks>Order is most important!</remarks>
        /// <returns><see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseMiddleware<LoggerMiddleware>();

            app.UseMiddleware<RequestMiddleware>();
            app.UseMiddleware<ResponseMiddleware>();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            return app;
        }
    }
}
