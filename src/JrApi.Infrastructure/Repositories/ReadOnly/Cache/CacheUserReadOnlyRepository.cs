using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Entities.Users;
using JrApi.Infrastructure.Core.Options;
using JrApi.SharedKernel;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace JrApi.Infrastructure.Repositories.ReadOnly.Cache;

public sealed class CacheUserReadOnlyRepository : BaseReadOnlyRepository<User>, IUserReadOnlyRepository
{
    private readonly IUserReadOnlyRepository _decorated;
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheOptions _options;

    public CacheUserReadOnlyRepository(
        ApplicationContext context,
        IUserReadOnlyRepository decorated,
        IDistributedCache distributedCache,
        IOptions<DistributedCacheOptions> options) : base(context)
    {
        _decorated = decorated;
        _distributedCache = distributedCache;
        _options = options.Value;
    }

    public override async Task<PagedList<User>> GetPagedAsync<TKey>(
        Expression<Func<User, TKey>> orderBy,
        int pageNumber = 0, 
        int pageSize = int.MaxValue, 
        CancellationToken cancellationToken = default)
    {
        string cachedUsers = (await _distributedCache.GetStringAsync(_options.UsersKey!, cancellationToken))!;

        if(string.IsNullOrEmpty(cachedUsers))
        {
            PagedList<User> response = await _decorated.GetPagedAsync<TKey>(orderBy, pageNumber, pageSize, cancellationToken);

            if (response is null)
                return response!;
            
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.ItemsExpirationMinutes)
            };

            await _distributedCache.SetStringAsync(_options.UsersKey!, JsonConvert.SerializeObject(response), cacheOptions, cancellationToken);
            return response;
        }
        return JsonConvert.DeserializeObject<PagedList<User>>(cachedUsers)!;
    }

    public override async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        string key = DistributedCacheOptions.GetEntityByIdKey(_options.UserByIdKey!, id);
        string cachedUser = (await _distributedCache.GetStringAsync(key, cancellationToken))!;

        if(string.IsNullOrEmpty(cachedUser))
        {
            User response = await _decorated.GetByIdAsync(id, cancellationToken);

            if (response is null)
                return response!;
            
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.ItemByIdExpirationMinutes)
            };

            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(response), cacheOptions, token: cancellationToken);
            return response;
        }
        return JsonConvert.DeserializeObject<User>(cachedUser)!;
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _decorated.EmailExistsAsync(email, cancellationToken);
    }
}