using Microsoft.Extensions.DependencyInjection;
using PowerCalculator.Application.Factory;
using PowerCalculator.Application.Interfaces;
using PowerCalculator.Application.Services;
using System.Reflection;

namespace PowerCalculator.Application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Dependency injection of Application Layer.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<IPowerPlantFactory, PowerPlantFactory>();
            services.AddTransient<IPowerPlanService, PowerPlanService>();

            return services;

        }
    }
}
