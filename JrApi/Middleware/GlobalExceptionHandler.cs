using System;
using System.Net;
using System.Text.Json;

namespace JrApi.Middleware
{
    public sealed class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //Logic
                await _next(context);
            } 
            catch(Exception e) 
            {
                //Error Log
                _logger.LogError(e, e.Message);
                await Handle(context, e);
            }
        }

        // Method referes a error 500
        private static async Task Handle(HttpContext context, Exception e)
        {
            // Error Status Code
            context.Response.StatusCode = 500;
            //Error Content Type
            context.Response.ContentType = "application/json";

            // Creating a Error Message
            var errorMessage = JsonSerializer.Serialize(
            new {
                Messages = e.Message,
                context.Response.StatusCode
            });

        //Write on Json error Message
        await context.Response.WriteAsync(errorMessage);
        } 
    }
}
