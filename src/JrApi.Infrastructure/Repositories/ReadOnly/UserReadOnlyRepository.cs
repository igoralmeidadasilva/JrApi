using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Domain.Entities.Users;

namespace JrApi.Infrastructure.Repositories.ReadOnly;

public sealed class UserReadOnlyRepository : BaseReadOnlyRepository<User>, IUserReadOnlyRepository
{
    public UserReadOnlyRepository(ApplicationContext context) : base(context) { }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        // return await Context.Users!.AsNoTracking().AnyAsync(x => x.Email!.Equals(email), cancellationToken);
        throw new NotImplementedException();
    }
}