using Microsoft.Extensions.DependencyInjection;
using PowerCalculator.Application.Interfaces.Infrastructure;
using PowerCalculator.Infrastructure.Services;

namespace PowerCalculator.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationService, ConfigurationService>();

            return services;
        }
    }

}
