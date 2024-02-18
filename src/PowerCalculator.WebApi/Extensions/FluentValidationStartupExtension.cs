using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using PowerCalculator.Application.Models.Response;
using PowerCalculator.Application.Validator;
using PowerCalculator.Domain.Models;
using System.Net;

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
                options.InvalidModelStateResponseFactory = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                    // Perform logging here.


                    var errorDetails = new List<ErrorDetail>();

                    var invalidItems = context.ModelState.Where(x => x.Value!.Errors.Any());

                    foreach (var item in invalidItems)
                    {
                        errorDetails.Add(new ErrorDetail(item.Key, item.Value!.Errors[0].ErrorMessage));
                    }

                    return new BadRequestObjectResult(new ErrorResponse
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Type = "ValidationException",
                        Title = "One or more validation errors occurred.",
                        Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}",
                        ErrorCode = "0000",
                        Erros = errorDetails
                    });
                };
            });

            return services;
        }
    }
}
