using Microsoft.Extensions.DependencyInjection;
using PowerCalculator.Application.Interfaces.Infrastructure;
using PowerCalculator.Infrastructure.Services;

namespace PowerCalculator.Infrastructure
{
    /// <summary>
    /// Dependency injection of Infrastructure Layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Dependency injection of Infrastructure Layer.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> for adding services.</param>
        /// <returns>Service collection configured.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationService, ConfigurationService>();

            return services;
        }
    }

}
