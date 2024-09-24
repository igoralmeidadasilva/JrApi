using JrApi.Domain.Entities.Users;

namespace JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;

public interface IUserReadOnlyRepository : IReadOnlyRepository<User>
{
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
}
