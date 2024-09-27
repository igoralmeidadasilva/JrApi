using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Entities.Users;
using JrApi.Infrastructure.Core.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace JrApi.Infrastructure.Repositories.Persistence.Cache;

public sealed class CacheUserPersistenceRepository : BasePersistenceRepository<User>, IUserPersistenceRepository
{
    private readonly IUserPersistenceRepository _decorated;
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheOptions _options; 

    public CacheUserPersistenceRepository(
        ApplicationContext context, 
        IUserPersistenceRepository decorated, 
        IDistributedCache distributedCache,
        IOptions<DistributedCacheOptions> options) : base(context)
    {
        _decorated = decorated;
        _distributedCache = distributedCache;
        _options = options.Value;
    }


    public override void Insert(User user)
    {
        _distributedCache.Remove(_options.UsersKey);
        _decorated.Insert(user);
    }


    public override void InsertRange(IEnumerable<User> users)
    {
        _distributedCache.Remove(_options.UsersKey);
        _decorated.InsertRange(users);
    }

    public override void Update(User user)
    {
        _distributedCache.Remove(DistributedCacheOptions.GetEntityByIdKey(_options.UserByIdKey, user.Id));
        _decorated.Update(user);
    }

    public override void Delete(Guid id)
    {
        _distributedCache.Remove(DistributedCacheOptions.GetEntityByIdKey(_options.UserByIdKey, id));
        _decorated.Delete(id);
    }

}
