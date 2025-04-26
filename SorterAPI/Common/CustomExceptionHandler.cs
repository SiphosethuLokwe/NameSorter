using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace SorterAPI.Common
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger;

        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            int statusCode = exception switch
            {
                ArgumentNullException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,
                TimeoutException => StatusCodes.Status408RequestTimeout,
                InvalidOperationException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            string title = exception switch
            {
                ArgumentNullException => "Bad Request - Missing or Invalid Data",
                ArgumentException => "Bad Request - Invalid Argument",
                TimeoutException => "Request Timeout",
                InvalidOperationException => "Invalid Operation",
                _ => "Internal Server Error"
            };

            _logger.LogError(exception, "Unhandled exception caught: {Message}", exception.Message);

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = exception.GetType().Name,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };

            // Add custom extensions
            problemDetails.Extensions["requestId"] = httpContext.TraceIdentifier;
            problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? httpContext.Features.Get<IHttpActivityFeature>()?.Activity?.Id;
            problemDetails.Extensions["timestamp"] = DateTime.UtcNow.ToString("o"); 

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
