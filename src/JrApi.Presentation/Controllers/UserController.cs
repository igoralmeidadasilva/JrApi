using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Application.Commands.Users.DeleteUser;
using JrApi.Application.Commands.Users.UpdateUser;
using JrApi.Application.Queries.Users.GetAllUsers;
using JrApi.Application.Queries.Users.GetUserById;
using JrApi.Domain.Core.Interfaces.Services;
using JrApi.Domain.Entities.Users;
using JrApi.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JrApi.Presentation.Controllers;

[Route("/api/[controller]")]
[ApiController]
[AllowAnonymous]
public sealed class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IMediator _mediator;
    public UserController(ILogger<UserController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());

        if (users.Any())
        {
            return Ok(users);
        }
        else
        {
            return NotFound("Is Empty");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetItemById(int id)
    {

        var user = await _mediator.Send(new GetUserByIdQuery { Id = id });

        if (user is null)
        {
            return NotFound("User not found");
        }
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Insert(CreateUserCommand command)
    {

        //var response = await _mediator.Send(command);

        //if(response.IsFailure)
        //{
        //    return BadRequest(response.Errors);
        //}

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> Update([FromBody] UpdateUserCommand userToUpdate, int id)
    {
        userToUpdate.Id = id;
        var user = await _mediator.Send(userToUpdate);
        if (user is null)
        {
            return NotFound("User not found");
        }
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var user = await _mediator.Send(new DeleteUserCommand { Id = id });
        if (user)
        {
            return Ok(user);
        }
        else
        {
            return BadRequest("User not found");
        }
    }
}
