using JrApi.Domain.Core.Abstractions;

namespace JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;

public interface IReadOnlyRepository<TEntity> where TEntity : Entity<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    // TODO: Debuggar esse m�todo
    Task<IEnumerable<TEntity>> GetByWhereAsync(Func<IEnumerable<TEntity>> func, CancellationToken cancellationToken = default);
}
