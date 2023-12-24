using JrApi.Domain.Interfaces.Repositories;
using JrApi.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace JrApi.Infrastructure.Repository.Caches
{
    /// <summary>
    /// Cache Repository for Users that uses the Decorator pattern to add caching functionality to the base repository.
    /// </summary>
    public sealed class CachingUserRepository : IDbRepository<UserModel>
    {
        private readonly UserRepository _decorated;
        private readonly IDistributedCache _distributedCache;
        private const int ITEMS_EXPIRATION_TIME = 5; 
        private const int ITEM_BY_ID_EXPIRATION_TIME = 2;
        private const string ITEMS_KEY = "user-all";
        private const string ITEM_BY_ID_KEY = "user-{0,0}"; 
        
        public CachingUserRepository(UserRepository decorated, IDistributedCache distributedCache)
        {
            _decorated = decorated;
            _distributedCache = distributedCache;
        }

        public async Task<IEnumerable<UserModel>> GetItems()
        {
            string key = ITEMS_KEY;
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
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(ITEMS_EXPIRATION_TIME)
                };

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(enumerable), cacheOptions);
                return enumerable;
            }
            enumerable = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(cachedUser)!;
            return enumerable!;
        }

        public async Task<UserModel> GetItemById(int id)
        {
            string key = string.Format(ITEM_BY_ID_KEY, id);
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
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(ITEM_BY_ID_EXPIRATION_TIME)
                };
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(user), cacheOptions);
                return user;
            }
            user = JsonConvert.DeserializeObject<UserModel>(cachedUser);
            return user!;
        }

        public UserModel Insert(UserModel item)
        {
            string key = ITEMS_KEY;
            _distributedCache.Remove(key);
            return _decorated.Insert(item);
        }

        public async Task<UserModel> Update(UserModel itemUpdate)
        {
            string key = string.Format(ITEM_BY_ID_KEY, itemUpdate.Id);
            _distributedCache.Remove(key);
            return await _decorated.Update(itemUpdate);
        }

        public async Task<bool> Delete(int id)
        {
            string key = string.Format(ITEM_BY_ID_KEY, id);
            _distributedCache.Remove(key);
            return await _decorated.Delete(id);
        }
    }
}
