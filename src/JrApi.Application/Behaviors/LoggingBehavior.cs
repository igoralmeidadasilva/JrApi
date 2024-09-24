using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace JrApi.Application.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        DateTime timestamp = DateTime.Now;
        Stopwatch stopwatch = Stopwatch.StartNew();

        try
        {
            _logger.LogInformation("Starting request: {RequestName} at {Timestamp}",
                requestName,
                timestamp);
            TResponse? response = await next();
            _logger.LogInformation("Request {RequestName} successful.",
                requestName);

            return response;
        }
        catch (Exception)
        {
            _logger.LogError("Request {RequestName} failed.",
                requestName);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Ending request: {RequestName} at {Timestamp} - duration: {Stopwatch} miliseconds.",
                requestName,
                timestamp,
                stopwatch.ElapsedMilliseconds);
        }
    }
}
