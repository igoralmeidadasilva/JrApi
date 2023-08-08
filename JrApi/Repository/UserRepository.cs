using JrApi.Data;
using JrApi.Models;
using JrApi.Repository.Interfaces;
using JrApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace JrApi.Repository
{
    // UserRepository class implementing the generic IDbRepository interface
    public class UserRepository : IDbRepository<UserModel> //FEEDBACK: not entirely necessary, but it's a fancy thing to always set as 'sealed' classes you know you wont inherit from.
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
            var users = await _dbContext.Users.ToListAsync(); //FEEDBACK: if this is a readonly operation, you can use AsNoTracking() to improve performance.
            // Returning the collection
            return users;
        }
        
        // Asynchronous method that returns a record from the Users table, this method receives a user ID
        public async Task<UserModel> SelectById(int id)
        {
            // Searching the database for the record by ID
            var user = await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);
            // If the user is null, the return will be null, otherwise the user will be returned
            if (user == null) //FEEDBACK: you don't need to use the else statement here. 'user' will be null if the Id don't exist in the database. You could just return 'user' directly.
            {
                return null!;
            }
            else
            {
                return user;
            }
        }

        // Asynchronous method that inserts a user into the database, this method receives the user and returns the user
        public async Task<UserModel> Insert(UserModel user)
        {
            // Static user validation method, if it returns true the user is created, added and committed to the database
            // Regardless of whether or not the commit is successful, the user will be returned
            if(UserValidation.Validation(user)) //FEEDBACK: this is not right use of validation logic, either the correct location to do it. You should validate the entity before calling repository, and you want to return a message in the case of failing validations. Furthermore, is not the case of doing an if/else statement like you did. You should first check for a validation, get the error messages and do an early if statement returning the appropriates messages to the user as a 400 http status.
            {
                // Inserting on Database
                await _dbContext.Users.AddAsync(user); //FEEDBACK: The AddAsync method is just helpful in a particular case. Read the documentation to understand when to use it. 99% of cases you go for standard 'Add' method
                // Committing changes
                await _dbContext.SaveChangesAsync();
                return user;
            }
            else{
                return user;
            }
        }

        // Asynchronous method that updates the information of a record in the database, this method receives a user with the new data and their ID. 
        // This method returns the user or null
        public async Task<UserModel> Update(UserModel user, int id) //FEEDBACK: you don't need the id argument here, because the UserModel already implements an Id. You can just use the Id from the user argument, or use the int argument. Choose between both.
        {
            // Searching User by Id
            var userUpdate = await SelectById(id);
            // If the searched user is null, the method ends and returns null
            if(userUpdate == null) //FEEDBACK: you don't need to use the else statement here. 'userUpdate' will be null if the Id don't exist in the database. You could just return 'userUpdate' directly.
            {
                return null!;                
            }
            else
            {
                // Updating the user's data in the database
                userUpdate.Name = user.Name;
                userUpdate.LastName = user.LastName;
                userUpdate.BirthDate = user.BirthDate;
                // Static validation method, if it is true, the operation is performed and the user is returned, otherwise the user is
                // returned without performing the operation
                if(UserValidation.Validation(userUpdate)){  //FEEDBACK: same feedback as the validation use-case in the Insert method above
                    // Updating User
                    _dbContext.Users.Update(userUpdate);
                    // Committing changes
                    await _dbContext.SaveChangesAsync();
                    return userUpdate;
                }
                else
                {
                    return userUpdate;
                }
            }
        }

        // Asynchronous method that deletes a user from the database, this method receives the user ID and returns true on success and false on failure
        public async Task<bool> Delete(int id)
        {
            // Searches for the user in the database
            var userDelete = await SelectById(id);
            // If the search returns null, it returns false, otherwise it returns true
            if(userDelete == null) //FEEDBACK: same as above to check if null to return null
            {
                return false;
            }
            else
            {
                // Deletes the user
                _dbContext.Users.Remove(userDelete);
                // Committing changes
                await _dbContext.SaveChangesAsync(); //FEEDBACK: SaveChangesAsync returns how many lines were affected. You may want to check if an actual amount of lines were affected before returning true for the Delete operation
                return true;
            }
        }
    }
}
