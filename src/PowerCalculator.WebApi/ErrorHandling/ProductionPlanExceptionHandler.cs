using Microsoft.AspNetCore.Diagnostics;
using PowerCalculator.Application.Models.Response;
using PowerCalculator.Domain.Exceptions;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerCalculator.WebApi.ErrorHandling
{
    /// <summary>
    /// Handling exceptions from Domain.
    /// </summary>
    public class ProductionPlanExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ProductionPlanExceptionHandler> _logger;

        /// <summary>
        /// Create new instance of <see cref="ProductionPlanExceptionHandler"/>.
        /// </summary>
        /// <param name="logger"></param>
        public ProductionPlanExceptionHandler(ILogger<ProductionPlanExceptionHandler> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ProductionPlanBaseException)
            {
                _logger.LogWarning(exception.Message);

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = exception.GetType().Name,
                    Title = exception.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                    ErrorCode = ((ProductionPlanBaseException)exception).ErrorCode
                },
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });

                return true;
            }

            return false;
        }
    }
}
