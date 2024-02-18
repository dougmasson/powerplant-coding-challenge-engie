using Asp.Versioning;

namespace PowerCalculator.WebApi.Extensions
{
    /// <summary>
    /// Configuration of ApiVersioning.
    /// </summary>
    public static class ApiVersioningStartupExtension
    {
        /// <summary>
        /// Configure Version of controllers.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> for adding services.</param>
        /// <returns>Service collection configured.</returns>
        public static IServiceCollection AddApiVersioningOptions(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
