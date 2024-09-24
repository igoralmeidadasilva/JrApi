using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Domain.Entities.Users;

namespace JrApi.Infrastructure.Repositories.Persistence;

public sealed class UserPersistenceRepository : BasePersistenceRepository<User>, IUserPersistenceRepository
{
    public UserPersistenceRepository(ApplicationContext context) : base(context)
    {
    }
}