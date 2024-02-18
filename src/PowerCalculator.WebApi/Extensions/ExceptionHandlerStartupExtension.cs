using PowerCalculator.WebApi.ErrorHandling;

namespace PowerCalculator.WebApi.Extensions
{
    /// <summary>
    /// Configuration of ExceptionHandler.
    /// </summary>
    public static class ExceptionHandlerStartupExtension
    {
        /// <summary>
        /// Add ExceptionHandler.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> for adding services.</param>
        /// <returns>Service collection configured.</returns>
        public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
        {
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<ProductionPlanExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            services.AddProblemDetails();

            return services;
        }
    }
}
