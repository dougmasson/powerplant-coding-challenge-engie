using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using PowerCalculator.Application.Validator;
using PowerCalculator.Domain.Models;

namespace PowerCalculator.WebApi.Extensions
{
    /// <summary>
    /// Configuration of FluentValidation.
    /// </summary>
    public static class FluentValidationStartupExtension
    {
        /// <summary>
        /// Configure FluentValidation and AutoValidation.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> for adding services.</param>
        /// <returns>Service collection configured.</returns>
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<ProductionPlanRequestValidator>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errorDetails = new List<ErrorDetail>();

                    var invalidItems = actionContext.ModelState.Where(x => x.Value!.Errors.Any());

                    foreach (var item in invalidItems)
                    {
                        errorDetails.Add(new ErrorDetail(item.Key, item.Value!.Errors[0].ErrorMessage));
                    }

                    throw new Domain.Exceptions.ValidationException(errorDetails);
                };
            });

            return services;
        }
    }
}
