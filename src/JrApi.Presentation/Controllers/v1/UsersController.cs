using Asp.Versioning;
using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Application.Commands.Users.DeleteUser;
using JrApi.Application.Commands.Users.UpdateUser;
using JrApi.Application.Dtos;
using JrApi.Application.Queries.Users.GetAllUsers;
using JrApi.Application.Queries.Users.GetUserById;
using JrApi.Presentation.Routes;

namespace JrApi.Presentation.Controllers.v1;

[AllowAnonymous]
[ApiVersion("1.0")]
public sealed class UsersController : ApiController<UsersController>
{
    public UsersController(ILogger<UsersController> logger, IMediator mediator) : base(logger, mediator)
    { }

    [HttpGet(ApiRoutes.Users.GET_ALL)]
    [ProducesResponseType(typeof(IEnumerable<GetAllUsersDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var response = await Mediator.Send(new GetAllUsersQuery());
        return response.IsSuccess ? Ok(response.Value!.Users) : GenerateErrorResponse(response);
    }

    [HttpGet(ApiRoutes.Users.GET_BY_ID)]
    [ProducesResponseType(typeof(GetUserByIdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var response = await Mediator.Send(new GetUserByIdQuery(userId));
        return response.IsSuccess ? Ok(response.Value!.User) : GenerateErrorResponse(response);
    }

    [HttpPost(ApiRoutes.Users.INSERT)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Insert(CreateUserCommand command)
    {
        var response = await Mediator.Send(command);
        return response.IsSuccess ? Created() : GenerateErrorResponse(response);
    }

    [HttpPut(ApiRoutes.Users.UPDATE)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid userId, UpdateUserCommand command)
    {
        command.Id = userId;

        var response = await Mediator.Send(command);
        return response.IsSuccess ? NoContent() : GenerateErrorResponse(response);
    }

    [HttpDelete(ApiRoutes.Users.DELETE)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid userId)
    {
        var response = await Mediator.Send(new DeleteUserCommand(userId));
        return response.IsSuccess ? NoContent() : GenerateErrorResponse(response);
    }

}
