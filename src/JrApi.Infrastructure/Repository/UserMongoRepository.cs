using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using JrApi.Infrastructure.Data.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JrApi.Infrastructure.Services
{
    public sealed class UserMongoRepository : IMongoDbRepository<UserModel>
    {
        private readonly IMongoCollection<UserModel> _collection;
        public UserMongoRepository(IOptions<UserDataBaseMongoSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _collection = database.GetCollection<UserModel>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<IEnumerable<UserModel>> GetItems()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<UserModel> GetItemById(int id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public UserModel Insert(UserModel item)
        {
            var lastUserId = _collection.Find(Builders<UserModel>.Filter.Empty)
                .SortByDescending(u => u.Id)
                .Limit(1)
                .FirstOrDefault()?.Id ?? 0;

            var insert = new UserModel
            {
                Id = lastUserId + 1,
                Name = item.Name,
                LastName = item.LastName,
                BirthDate = item.BirthDate
            };


            _collection.InsertOne(insert);
            return insert;
        }

        public async Task<UserModel> Update(UserModel itemUpdate)
        {
            await _collection.ReplaceOneAsync(x => x.Id == itemUpdate.Id, itemUpdate);
            return itemUpdate;
        }
        
        public async Task<bool> Delete(int id)
        {
            var result = await GetItemById(id);

            if(result is null)
            {
                return false;
            }

            await _collection.DeleteOneAsync(x => x.Id == id);
            return true;
        }
    }
}
