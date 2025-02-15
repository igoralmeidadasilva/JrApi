using Asp.Versioning;
using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Application.Queries.Users.GetUserById;
using JrApi.Application.Queries.Users.GetUsersPaged;
using JrApi.Presentation.Api.Core.Abstractions;
using JrApi.Presentation.Api.Routes;
using JrApi.SharedKernel.PageList;
using JrApi.SharedKernel.Results;

namespace JrApi.Presentation.Controllers.v1;

[AllowAnonymous]
[ApiVersion("1.0")]
public sealed class UsersController : ApiController<UsersController>
{
    public UsersController(ILogger<UsersController> logger, IMediator mediator) : base(logger, mediator) { }

    [HttpGet(ApiRoutes.Users.GET_USERS_PAGED)]
    [ProducesResponseType(typeof(PagedResponse<GetUsersPagedQueryResponseItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsersPaged(int pageNumber = 1, int pageSize = 2)
    {
        var request = new GetUsersPagedQuery(pageNumber, pageSize);
        var response = await Mediator.Send(request);
        return response.IsSuccess ? Ok(response.Value) : GenerateErrorResponse(response);
    }

    [HttpGet(ApiRoutes.Users.GET_BY_ID)]
    [ProducesResponseType(typeof(GetUserByIdQueryResponseItem ), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var response = await Mediator.Send(new GetUserByIdQuery(userId));
        return response.IsSuccess ? Ok(response.Value) : GenerateErrorResponse(response);
    }

    [HttpPost(ApiRoutes.Users.CREATE)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUser(CreateUserCommand command)
    {
        var response = await Mediator.Send(command);
        return response.IsSuccess ? Created(nameof(GetUserById), response.Value) : GenerateErrorResponse(response);
    }

    // [HttpPut(ApiRoutes.Users.UPDATE)]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
    // public async Task<IActionResult> Update(Guid userId, UpdateUserCommand command)
    // {
    //     command.Id = userId;

    //     var response = await Mediator.Send(command);
    //     return response.IsSuccess ? NoContent() : GenerateErrorResponse(response);
    // }

    // [HttpDelete(ApiRoutes.Users.DELETE)]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
    // public async Task<IActionResult> Delete(Guid userId)
    // {
    //     var response = await Mediator.Send(new DeleteUserCommand(userId));
    //     return response.IsSuccess ? NoContent() : GenerateErrorResponse(response);
    // }
}