using Asp.Versioning;
using JrApi.SharedKernel.Results;

namespace JrApi.Presentation.Core.Abstractions;

[ApiController]
[ApiVersion("2.0")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}")]
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
            EErrorType.NotFound => NotFound(isEmpty ? string.Empty : result.Errors),
            EErrorType.Validation => BadRequest(isEmpty ? string.Empty : result.Errors),
            EErrorType.Conflict => Conflict(isEmpty ? string.Empty : result.Errors),
            EErrorType.Forbidden => Forbid(isEmpty ? string.Empty : error.Message),
            EErrorType.Unauthorized => Unauthorized(isEmpty ? string.Empty : result.Errors),
            EErrorType.Unexpected => BadRequest(isEmpty ? string.Empty : result.Errors),
            _ => BadRequest(isEmpty ? string.Empty : result.Errors),
        };
    }
}