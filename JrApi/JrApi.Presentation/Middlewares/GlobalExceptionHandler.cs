using System.Net;
using System.Text.Json;
using FluentValidation;

namespace JrApi.Presentation.Middlewares
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            } 
            catch(ValidationException e)
            {
                await Handle(context, e);
            }
            catch (AggregateException e)
            {
                if (e.InnerException is ValidationException ve)
                {
                    await Handle(context, ve);
                }
                else
                {
                    await Handle(context, e);
                }
            }
            catch(Exception e) 
            {
                _logger.LogError(e, e.Message);
                await Handle(context, e);
            }
        }

        // Method referes a error 500
        private static async Task Handle(HttpContext context, Exception e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorMessage = JsonSerializer.Serialize(
                new
                {
                    Messages = e.Message.Split("\n"),
                    context.Response.StatusCode
                });

            await context.Response.WriteAsync(errorMessage);
        } 

        private static async Task Handle(HttpContext context, ValidationException e)
        {
            // Error Status Code
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var errorMessage = JsonSerializer.Serialize(
                new
                {
                    Messages = e.Message.Split("\n"),
                    context.Response.StatusCode
                });

            await context.Response.WriteAsync(errorMessage);
        }

        private static async Task Handle(HttpContext context, AggregateException e)
        {
            // Error Status Code
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorMessage = JsonSerializer.Serialize(
                new
                {
                    Messages = e.Message.Split("\n"),
                    context.Response.StatusCode
                });

            await context.Response.WriteAsync(errorMessage);
        }
    }
}
