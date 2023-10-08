using JrApi.Domain.Entities;
using JrApi.Infrastructure.Repository.Interfaces;
using JrApi.Infrastructure.Services;
using JrApi.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace JrApi.Infrastructure.Repository.Caches
{
    // This class is a decorator for the IDbRepository interface, and its responsibility is to handle caching in the Redis database service.
    public sealed class CachingUserMongoService : IMongoDbServices<UserModel>
    {
        private readonly UserMongoService _decorated;
        private readonly IDistributedCache _distributedCache;

        //These constants represent the expiration time for keys GetItemsKey and GetItemByIdKey, respectively.
        private const int GetItemsExpirationTime = 5;
        private const int GetItemByIdExpirationTime = 2;

        //These constants represent the Key's which will be sent to Redis.
        private const string GetItemsKey = "user-mongo-all";
        private const string GetItemByIdKey = "user-mongo-{0,0}"; // This particular key requires a string formatter in the declaration {0,0}.
        
        public CachingUserMongoService(UserMongoService decorated, IDistributedCache distributedCache)
        {
            _decorated = decorated;
            _distributedCache = distributedCache;
        }

        // Asynchronous method that returns all records in the Users table using the IEnumerable interface, But it utilizes 
        // the Redis cache service, inserting records into the cache if they are not present.
        public async Task<IEnumerable<UserModel>> GetItems()
        {
            string key = GetItemsKey;
            string? cachedUser = await _distributedCache.GetStringAsync(key);
            IEnumerable<UserModel> enumerable;

            if (string.IsNullOrEmpty(cachedUser))
            {
                enumerable = await _decorated.GetItems();

                if (enumerable is null)
                {
                    return enumerable!;
                }

                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(GetItemsExpirationTime)
                };

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(enumerable), cacheOptions);
                return enumerable;
            }

            enumerable = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(cachedUser)!;
            return enumerable!;
        }

        // Asynchronous method that returns a records in the Users table, this methos receives a user id, but it utilizes 
        // the Redis cache service, inserting the record into the cache if they are not present.
        public async Task<UserModel> GetItemById(int id)
        {

            string key = string.Format(GetItemByIdKey, id);

            string? cachedUser = await _distributedCache.GetStringAsync(key);

            UserModel? user;

            if (string.IsNullOrEmpty(cachedUser))
            {
                user = await _decorated.GetItemById(id);

                if (user is null)
                {
                    return user!;
                }

                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(GetItemByIdExpirationTime)
                };

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(user), cacheOptions);
                return user;
            }
            user = JsonConvert.DeserializeObject<UserModel>(cachedUser);
            return user!;
        }

        public UserModel Insert(UserModel item)
        {
            string key = GetItemsKey;
            _distributedCache.Remove(key);
            return _decorated.Insert(item);
        }

        public async Task<UserModel> Update(UserModel itemUpdate)
        {
            string key = string.Format(GetItemByIdKey, itemUpdate.Id);
            _distributedCache.Remove(key);
            return await _decorated.Update(itemUpdate);
        }

        public async Task<bool> Delete(int id)
        {
             string key = string.Format(GetItemByIdKey, id);
            _distributedCache.Remove(key);
            return await _decorated.Delete(id);
        }
    }
}
