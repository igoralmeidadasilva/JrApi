using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JrApi.Application.Behaviors
{
    public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = request.GetType().Name;
            _logger.LogInformation($"[START] {requestName}");
            TResponse response;
            try 
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                response = await next();
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed; 
                _logger.LogInformation($"--- Time Span ---");
                _logger.LogInformation($"Requisition Time: {ts.Seconds} : {ts.Milliseconds} : {ts.Nanoseconds}");
            }
            finally
            {
                _logger.LogInformation($"[END] {requestName}");
            }
            return response;
        }
    }
}
