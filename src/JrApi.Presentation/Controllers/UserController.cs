using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Application.Commands.Users.DeleteUser;
using JrApi.Application.Commands.Users.UpdateUser;
using JrApi.Application.Queries.Users.GetAllUsers;
using JrApi.Application.Queries.Users.GetUserById;
using JrApi.Presentation.Core.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JrApi.Presentation.Controllers;

[Route("/api/[controller]")]
[AllowAnonymous]
public sealed class UserController : ApiController<UserController>
{
    public UserController(ILogger<UserController> logger, IMediator mediator) : base(logger, mediator)
    { }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllUsersQuery());
        return response.IsSuccess ? Ok(response.Value!.Users) : GenerateErrorResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetUserByIdQuery(id));
        return response.IsSuccess ? Ok(response.Value!.User) : GenerateErrorResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreateUserCommand command)
    {
        var response = await _mediator.Send(command);
        return response.IsSuccess ? Created() : GenerateErrorResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserCommand command)
    {
        var response = await _mediator.Send(command);
        return response.IsSuccess ? NoContent() : GenerateErrorResponse(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteUserCommand command)
    {
        var response = await _mediator.Send(command);
        return response.IsSuccess ? NoContent() : GenerateErrorResponse(response);
    }

}
