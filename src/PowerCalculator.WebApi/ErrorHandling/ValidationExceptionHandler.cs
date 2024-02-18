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
    public class ValidationExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ValidationExceptionHandler> _logger;

        /// <summary>
        /// Create new instance of <see cref="ValidationExceptionHandler"/>.
        /// </summary>
        /// <param name="logger"></param>
        public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ValidationException)
            {
                _logger.LogWarning(exception.Message);

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = exception.GetType().Name,
                    Title = exception.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                    ErrorCode = "0000",
                    Erros = ((ValidationException)exception).Errors
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
