using System.Diagnostics;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using JrApi.Models;
using JrApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JrApi.Controllers
{
    // Defining route for /api/usuarios
    // FEEDBACK: Use IActionResult so you get free of defining the actual return type for every endpoint method.
    [Route("/api/usuarios")]
    [ApiController]    
    public class UserController : ControllerBase
    {

        private readonly IDbRepository<UserModel> _user;
        private readonly IValidator<UserModel> _validator;
        private readonly ILogger<UserController> _logger;
        // Constructor that receives an implementation of the IUserRepository interface 
        public UserController(IDbRepository<UserModel> user, IValidator<UserModel> validator, ILogger<UserController> logger)
        {
            _user = user;
            _validator = validator;
            _logger = logger;
        }

        // Asynchronous HTTPGET method that is responsible for Selectin all records from the Database.
        // This method returns an Ok() if successful, otherwise returns a NotFound()
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {

            var users = await _user.GetItems();

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
            var user = await _user.GetItemById(id);

            // If the UserModel instance is not null 
            if(user != null)
            {
                return Ok(user);
            } 
            else 
            {
                return NotFound("User not found");
            }
        }

        // Asynchronous HTTPPOST method that is responsible for inserting a record into the Database. For user validation, this method uses the static class UserValidation 
        // This method returns an Created() if successful, otherwise returns a BadRequest().
        [HttpPost]
        public async Task<ActionResult<UserModel>> InsertAsync([FromBody] UserModel userToInsert)
        {
            ValidationResult result = await _validator.ValidateAsync(userToInsert);
            if(!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return BadRequest(result.Errors.Select(e => e.ErrorMessage).ToArray());
            }
            // Calling the repository method
            var user = _user.Insert(userToInsert);
            return Created("Successfully created", user); 
        }

        // Asynchronous HTTPPUT method that is responsible for updating a record into the Database. This method receives a user id and a instance of UserModel
        // to search for the record. For use validation, this method the static class UserValidation.
        // This method returns an Ok() if successful, if validation errors occur, return BadRequest and a JSON with the errors that occurred and return NotFound if the user is not found.
        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update([FromBody] UserModel userBody, int id)
        {
            //Validation
            ValidationResult result = await _validator.ValidateAsync(userBody);
            //Searching in Database the User
            var userUpdate = await _user.GetItemById(id);
            
            // Returning NotFound
            if(userUpdate == null){
                return NotFound("User not found");
            }

            // Returning BadRequest
            if(!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return BadRequest(result.Errors.Select(e => e.ErrorMessage).ToArray());
            }

            // Calling the repository method
            var user = await _user.Update(userBody, userUpdate);
            return Ok(user);
        }

        // Asynchronous HTTPDELETE method that is responsible for deleting a record into the database. this method receives a user id.
        // This method returns an Ok() if sucessfull, otherwise returns a BadRequest().
        [HttpDelete("id")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            // Calling the repository method
            var user = await _user.Delete(id);
            // If return of method is true
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
