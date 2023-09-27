using System;
using System.Diagnostics;

namespace JrApi.Middleware
{
    public class RequisitionTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequisitionTimeMiddleware> _logger;

        public RequisitionTimeMiddleware(RequestDelegate next, ILogger<RequisitionTimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                await _next(context);
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed; 
                _logger.LogInformation($"Requisition Time: {ts.Seconds} : {ts.Milliseconds} : {ts.Nanoseconds}"); 
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
            } 
        }
    }
}