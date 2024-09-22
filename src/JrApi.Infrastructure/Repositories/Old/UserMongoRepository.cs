//using JrApi.Domain.Core.Interfaces.Repositories.Mongo;
//using JrApi.Domain.Users;
//using JrApi.Infrastructure.Data.Settings;
//using Microsoft.Extensions.Options;
//using MongoDB.Bson;
//using MongoDB.Driver;

//namespace JrApi.Infrastructure.Repository
//{
//    public sealed class UserMongoRepository : IMongoRepository<User>
//    {
//        private readonly IMongoCollection<User> _collection;
//        public UserMongoRepository(IOptions<UserDataBaseMongoSettings> mongoDBSettings)
//        {
//            var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
//            var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
//            _collection = database.GetCollection<User>(mongoDBSettings.Value.CollectionName);
//        }

//        public async Task<IEnumerable<User>> GetItems()
//        {
//            return await _collection.Find(new BsonDocument()).ToListAsync();
//        }

//        public async Task<User> GetItemById(int id)
//        {
//            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
//        }

//        public User Insert(User item)
//        {
//            var lastUserId = _collection.Find(Builders<User>.Filter.Empty)
//                .SortByDescending(u => u.Id)
//                .Limit(1)
//                .FirstOrDefault()?.Id ?? 0;

//            var insert = new User
//            {
//                Id = lastUserId + 1,
//                Name = item.Name,
//                LastName = item.LastName,
//                BirthDate = item.BirthDate
//            };

//            _collection.InsertOne(insert);
//            return insert;
//        }

//        public async Task<User> Update(User itemUpdate)
//        {
//            await _collection.ReplaceOneAsync(x => x.Id == itemUpdate.Id, itemUpdate);
//            return itemUpdate;
//        }
        
//        public async Task<bool> Delete(int id)
//        {
//            var result = await GetItemById(id);

//            if(result is null)
//            {
//                return false;
//            }

//            await _collection.DeleteOneAsync(x => x.Id == id);
//            return true;
//        }
//    }
//}
