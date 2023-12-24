using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using JrApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JrApi.Infrastructure.Repository
{
    public sealed class UserRepository : IDbRepository<UserModel> 
    {
        private readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<IEnumerable<UserModel>> GetItems()
        {
            var users = await _dbContext.Users.AsNoTracking().ToListAsync(); 
            return users;
        }
        
        public async Task<UserModel> GetItemById(int id)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return user!; 
        }

        public UserModel Insert(UserModel user)
        {
            _dbContext.Users.Add(user); 
            _dbContext.SaveChanges();
            return user;

        }

        public async Task<UserModel> Update(UserModel userUpdate) 
        {
            _dbContext.Users.Update(userUpdate);
            await _dbContext.SaveChangesAsync();
            return userUpdate;
        }

        public async Task<bool> Delete(int id)
        {
            var userDelete = await GetItemById(id);
            if(userDelete is null)
            {
                return false;
            }
            _dbContext.Users.Remove(userDelete); 
            _dbContext.SaveChanges();
            return true;
        }
    }
}
