using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Application.Commands.Users.DeleteUser;
using JrApi.Application.Commands.Users.UpdateUser;
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

    [HttpPost]
    public async Task<IActionResult> Insert(CreateUserCommand command)
    {
        var response = await _mediator.Send(command);
        return response.IsSuccess ? Ok() : GenerateErrorResponse(response);
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
