using Microsoft.AspNetCore.Mvc;

namespace JrApi.Controllers
{
    // Defining route for /api/usuarios
    // FEEDBACK: Use IActionResult so you get free of defining the actual return type for every endpoint method.
    [Route("/api/usuarios")]
    [ApiController]
    public sealed class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        // Constructor that receives an implementation of the IUserRepository interface 
        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // Asynchronous HTTPGET method that is responsible for Selectin all records from the Database.
        // This method returns an Ok() if successful, otherwise returns a NotFound()
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            //var users = await _user.GetItems();
            var users = await _mediator.Send(new GetAllUsersQuery());

            // If sequence contains elements
            if(users.Any())
            {
                return Ok(users);
            } 
            else 
            {
                return NotFound("Is Empty");
            }
        }

        // Asynchronous HTTPGET method that is responsible for selecting one record from the Database. This method receives a user id to search for the record
        // This method returns an Ok() if successful, otherwise returns a NotFound()
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetItemById(int id)
        {
            // Calling the repository method
            //var user = await _user.GetItemById(id);
            var user = await _mediator.Send(new GetUserByIdQuery {Id = id});

            // If the UserModel instance is not null 
            if(user is null)
            {
                return NotFound("User not found");
            } 
            return Ok(user);
        }

        // Asynchronous HTTPPOST method that is responsible for inserting a record into the Database. For user validation, this method uses the static class UserValidation 
        // This method returns an Created() if successful, otherwise returns a BadRequest().
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

        // Asynchronous HTTPPUT method that is responsible for updating a record into the Database. This method receives a user id and a instance of UserModel
        // to search for the record. For use validation, this method the static class UserValidation.
        // This method returns an Ok() if successful, if validation errors occur, return BadRequest and a JSON with the errors that occurred and return NotFound if the user is not found.
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

        // Asynchronous HTTPDELETE method that is responsible for deleting a record into the database. this method receives a user id.
        // This method returns an Ok() if sucessfull, otherwise returns a BadRequest().
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
