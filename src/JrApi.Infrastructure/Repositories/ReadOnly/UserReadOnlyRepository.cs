using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Entities.Users;
using JrApi.Infrastructure.Context;

namespace JrApi.Infrastructure.Repositories.ReadOnly;

public sealed class UserReadOnlyRepository : BaseReadOnlyRepository<User>, IUserReadOnlyRepository
{
    public UserReadOnlyRepository(ApplicationContext context) : base(context)
    {
    }

    public Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}