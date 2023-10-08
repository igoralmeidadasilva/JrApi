using JrApi.Application.Commands.MongoDB;
using JrApi.Application.Queries.MongoDB;
using JrApi.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JrApi.Presentation.Controllers
{
    [Route("/api/mongo")]
    [ApiController]
    public sealed class UserMongoController : ControllerBase
    {
        private readonly ILogger<UserMongoController> _logger;
        private readonly IMongoDbServices<UserModel> _context;

        private readonly IMediator _mediator;
        public UserMongoController(ILogger<UserMongoController> logger, IMongoDbServices<UserModel> context, IMediator mediator)
        {
            _logger = logger;
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersMongoQuery());
            if(!result.Any())
            {
                return NotFound("Is Empty");
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdMongoQuery{ Id = id});
            if(result is null)
            {
                return NotFound("User not found");
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<UserModel> Insert([FromBody] CreateUserMongoCommand userToInsert)
        {
            var user = _mediator.Send(userToInsert);
            if(user is null)
            {
                return BadRequest();
            }
            return Created("Successfully created", user.Result); 
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update([FromBody] UpdateUserMongoCommand userToUpdate, int id)
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
            var result = await _mediator.Send(new DeleteUserMongoCommand{ Id = id});
            if(!result)
            {
               return NotFound("User not found"); 
            }
            return Ok(result);
        }
    }
}
