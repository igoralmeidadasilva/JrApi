using JrApi.Application.Commands.Users.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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

    [HttpPost]
    public async Task<IActionResult> Insert(CreateUserCommand command)
    {
        var response = await _mediator.Send(command);

        if(response.IsFailure)
        {
            return BadRequest(response.Errors);
        }

        return Ok();
    }

}
