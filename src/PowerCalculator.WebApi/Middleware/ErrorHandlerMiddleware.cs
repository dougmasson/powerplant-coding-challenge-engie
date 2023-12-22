using PowerCalculator.Application.Models.Response;
using PowerCalculator.Domain.Exceptions;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerCalculator.WebApi.Middleware
{
    /// <summary>
    /// Handle Expecetion into Rest API.
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, Serilog.ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex is ProductionPlanBaseException)
                {
                    _logger.Warning(ex.Message);
                }
                else
                {
                    _logger.Error(ex, ex.Message);
                }

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            ErrorResponse errorDetail = new ErrorResponse
            {
                Status = context.Response.StatusCode,
                Type = exception.GetType().Name,
                Instance = context.Request.Path
            };

            if (exception is ProductionPlanBaseException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                errorDetail.Status = (int)HttpStatusCode.BadRequest;
                errorDetail.Title = exception.Message;
                errorDetail.ErrorCode = ((ProductionPlanBaseException)exception).ErrorCode;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                errorDetail.Status = (int)HttpStatusCode.InternalServerError;
                errorDetail.Title = "Something went wrong";
                errorDetail.Detail = exception.Message;
            }

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorDetail, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            }));
        }
    }
}
