using Microsoft.Extensions.Options;
using PowerCalculator.WebApi.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace PowerCalculator.WebApi.Extensions
{
    /// <summary>
    /// Configuration of Swagger.
    /// </summary>
    public static class SwaggerStartupExtension
    {
        /// <summary>
        /// Configure swagger in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> for adding services.</param>
        /// <returns>Service collection configured.</returns>
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }

        /// <summary>
        /// Configure swagger in <see cref="WebApplication"/>.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>WebApplication configured.</returns>
        public static IApplicationBuilder UseSwaggerConfiguration(this WebApplication app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                var descriptions = app.DescribeApiVersions();

                // Build a swagger endpoint for each discovered API version
                foreach (var description in descriptions)
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });

            return app;
        }

    }
}
