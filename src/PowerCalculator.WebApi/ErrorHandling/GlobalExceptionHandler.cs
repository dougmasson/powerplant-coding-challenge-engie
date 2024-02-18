using Microsoft.AspNetCore.Diagnostics;
using PowerCalculator.Application.Models.Response;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerCalculator.WebApi.ErrorHandling
{
    /// <summary>
    /// Handling global exceptions.
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        /// <summary>
        /// Create new instance of <see cref="GlobalExceptionHandler"/>.
        /// </summary>
        /// <param name="logger"></param>
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);

            await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = exception.GetType().Name,
                Title = "Something went wrong",
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            },
            new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            return true;
        }
    }
}
