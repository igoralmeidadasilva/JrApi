using JrApi.Application.Commands.Users.CreateUser;
using JrApi.Application.Commands.Users.DeleteUser;
using JrApi.Application.Commands.Users.UpdateUser;
using JrApi.Application.Queries.Users;
using JrApi.Application.Queries.Users.GetAllUsers;
using JrApi.Application.Queries.Users.GetUserById;
using JrApi.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JrApi.Controllers
{
    [Route("/api/usuarios")]
    [ApiController]
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
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());

            if(users.Any())
            {
                return Ok(users);
            } 
            else 
            {
                return NotFound("Is Empty");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetItemById(int id)
        {

            var user = await _mediator.Send(new GetUserByIdQuery {Id = id});

            if(user is null)
            {
                return NotFound("User not found");
            } 
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserModel> Insert([FromBody] CreateUserCommand userToInsert)
        {
            var user = _mediator.Send(userToInsert);
            if(user.Result is null)
            {
                return BadRequest();
            }
            return Created("Successfully created", user.Result); 
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update([FromBody] UpdateUserCommand userToUpdate, int id)
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
}
