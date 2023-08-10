using JrApi.Data;
using JrApi.Models;
using JrApi.Repository.Interfaces;
using JrApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace JrApi.Repository
{
    // UserRepository class implementing the generic IDbRepository interface
    public sealed class UserRepository : IDbRepository<UserModel> //FEEDBACK: not entirely necessary, but it's a fancy thing to always set as 'sealed' classes you know you wont inherit from.
    {
        // DataBase Context
        private readonly UserDbContext _dbContext;

        // Contructor
        public UserRepository(UserDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        // Asynchronous method that returns all records in the Users table using the IEnumerable interface
        public async Task<IEnumerable<UserModel>> SelectAll()
        {
            // Searching for all records in the database
            var users = await _dbContext.Users.AsNoTracking().ToListAsync(); //FEEDBACK: if this is a readonly operation, you can use AsNoTracking() to improve performance.
            // Returning the collection
            return users;
        }
        
        // Asynchronous method that returns a record from the Users table, this method receives a user ID
        public async Task<UserModel> SelectById(int id)
        {
            // Searching the database for the record by ID
            var user = await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);
            // Returning the selected user
            return user!; //FEEDBACK: you don't need to use the else statement here. 'user' will be null if the Id don't exist in the database. You could just return 'user' directly.        
        }

        // Synchronous method that inserts one record in the User table, this method receives a User ID and returns the inserted User.
        public UserModel Insert(UserModel user)
        {
            //FEEDBACK: this is not right use of validation logic, either the correct location to do it. You should validate the entity before calling repository, and you want to return a message in the case of failing validations. Furthermore, is not the case of doing an if/else statement like you did. You should first check for a validation, get the error messages and do an early if statement returning the appropriates messages to the user as a 400 http status.
            _dbContext.Users.Add(user); //FEEDBACK: The AddAsync method is just helpful in a particular case. Read the documentation to understand when to use it. 99% of cases you go for standard 'Add' method
            _dbContext.SaveChanges();
            return user;

        }

        // Asynchronous method that updates a record in the database, this method receives the user in the database and the data that will be updated via JSON, 
        // this method returns the user that was updated
        public async Task<UserModel> Update(UserModel userBody, UserModel userUpdate) //FEEDBACK: you don't need the id argument here, because the UserModel already implements an Id. You can just use the Id from the user argument, or use the int argument. Choose between both.
        {
            //FEEDBACK: you don't need to use the else statement here. 'userUpdate' will be null if the Id don't exist in the database. You could just return 'userUpdate' directly.
            userUpdate.Name = userBody.Name;
            userUpdate.LastName = userBody.LastName;
            userUpdate.BirthDate = userBody.BirthDate;
            //FEEDBACK: same feedback as the validation use-case in the Insert method above
            _dbContext.Users.Update(userUpdate);
            await _dbContext.SaveChangesAsync();
            return userUpdate;
            
        }

        // Asynchronous method that deletes a user from the database, this method receives the user ID and returns true on success and false on failure
        public async Task<bool> Delete(int id)
        {
            // Searches for the user in the database
            var userDelete = await SelectById(id);
            if(userDelete == null)
            {
                return false;
            }
            // Deletes the user
            _dbContext.Users.Remove(userDelete); //FEEDBACK: same as above to check if null to return null  
            // Committing changes
            _dbContext.SaveChanges(); //FEEDBACK: SaveChangesAsync returns how many lines were affected. You may want to check if an actual amount of lines were affected before returning true for the Delete operation
            return true;
        }
    }
}
