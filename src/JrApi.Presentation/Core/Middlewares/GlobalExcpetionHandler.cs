using Microsoft.AspNetCore.Diagnostics;

namespace JrApi.Presentation.Core.Middlewares;

public sealed class GlobalExcpetionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExcpetionHandler> _logger;

    public GlobalExcpetionHandler(ILogger<GlobalExcpetionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", 
            exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server error"
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

}
