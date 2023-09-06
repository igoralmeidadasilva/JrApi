using JrApi.Models;
using JrApi.Repository.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace JrApi.Repository.Caches
{
    // This class is a decorator for the IDbRepository interface, and its responsibility is to handle caching in the Redis database service.
    public class CachingUserRepository : IDbRepository<UserModel>
    {
        private readonly UserRepository _decorated;
        private readonly IDistributedCache _distributedCache;

        //These constants represent the expiration time for keys GetItemsKey and GetItemByIdKey, respectively.
        private const int GetItemsExpirationTime = 5;
        private const int GetItemByIdExpirationTime = 2;

        //These constants represent the Key's which will be sent to Redis.
        private const string GetItemsKey = "user-all";
        private const string GetItemByIdKey = "user-{0,0}"; // This particular key requires a string formatter in the declaration {0,0}.
        
        public CachingUserRepository(UserRepository decorated, IDistributedCache distributedCache)
        {
            _decorated = decorated;
            _distributedCache = distributedCache;
        }

        // Asynchronous method that returns all records in the Users table using the IEnumerable interface, But it utilizes 
        // the Redis cache service, inserting records into the cache if they are not present.
        public async Task<IEnumerable<UserModel>> GetItems()
        {
            // Redis Key
            string key = GetItemsKey;
            // Cache query
            string? cachedUser = await _distributedCache.GetStringAsync(key);
            // Declaration of return
            IEnumerable<UserModel> enumerable;
            // This "if" statement is true here only if the records are in the cache; otherwise, it is false.
            if (string.IsNullOrEmpty(cachedUser))
            {
                enumerable = await _decorated.GetItems();
                // This "if" statement returns null if the database query does not return any records. 
                if (enumerable is null)
                {
                    return enumerable!;
                }

                // Cache option for set Expiration time
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(GetItemsExpirationTime)
                };

                // Set in redis the records of database
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(enumerable), cacheOptions);
                return enumerable;
            }
            // Deserialized redis query
            enumerable = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(cachedUser)!;
            return enumerable!;
        }

        // Asynchronous method that returns a records in the Users table, this methos receives a user id, but it utilizes 
        // the Redis cache service, inserting the record into the cache if they are not present.
        public async Task<UserModel> GetItemById(int id)
        {
            // Redis Key
            string key = string.Format(GetItemByIdKey, id);
            // Cache query
            string? cachedUser = await _distributedCache.GetStringAsync(key);
            // Declaration of return
            UserModel? user;
            // This "if" statement is true here only if the records are in the cache; otherwise, it is false.
            if (string.IsNullOrEmpty(cachedUser))
            {
                user = await _decorated.GetItemById(id);
                // This "if" statement returns null if the database query does not return any records. 
                if (user is null)
                {
                    return user!;
                }
                // Cache option for set Expiration time
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(GetItemByIdExpirationTime)
                };
                // Set in redis the records of database
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(user), cacheOptions);
                return user;
            }
            user = JsonConvert.DeserializeObject<UserModel>(cachedUser);
            return user!;
        }

        // This method inserts a record into the database and deletes the "GetItemsKey" from the cache. 
        public UserModel Insert(UserModel item)
        {
            string key = GetItemsKey;
            _distributedCache.Remove(key);
            return _decorated.Insert(item);
        }

        // This method updates a record into the database and deletes the "GetItemByIdKey" from the cache.
        public async Task<UserModel> Update(UserModel itemBody, UserModel itemUpdate)
        {
            string key = string.Format(GetItemByIdKey, itemUpdate.Id);
            _distributedCache.Remove(key);
            return await _decorated.Update(itemBody, itemUpdate);
        }

        // This method removee a record into the database and deletes the "GetItemByIdKey" from the cache.
        public async Task<bool> Delete(int id)
        {
             string key = string.Format(GetItemByIdKey, id);
            _distributedCache.Remove(key);
            return await _decorated.Delete(id);
        }
    }
}
