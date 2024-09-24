namespace JrApi.Presentation.Core.Abstractions;

[ApiController]
[Route("api")]
public abstract class ApiController<TController> : ControllerBase
{
    protected readonly ILogger<TController> Logger;
    protected readonly IMediator Mediator;

    protected ApiController(ILogger<TController> logger, IMediator mediator)
    {
        Logger = logger;
        Mediator = mediator;
    }

    protected IActionResult GenerateErrorResponse(Result result, bool isEmpty = false)
    {
        Error error = result.FirstError();

        return error.Type switch
        {
            ErrorType.NotFound => NotFound(isEmpty ? string.Empty : result.Errors),
            ErrorType.Validation => BadRequest(isEmpty ? string.Empty : result.Errors),
            ErrorType.Conflict => Conflict(isEmpty ? string.Empty : result.Errors),
            ErrorType.Forbidden => Forbid(isEmpty ? string.Empty : error.Message),
            ErrorType.Unauthorized => Unauthorized(isEmpty ? string.Empty : result.Errors),
            ErrorType.Unexpected => BadRequest(isEmpty ? string.Empty : result.Errors),
            _ => BadRequest(isEmpty ? string.Empty : result.Errors),
        };

    }

}
