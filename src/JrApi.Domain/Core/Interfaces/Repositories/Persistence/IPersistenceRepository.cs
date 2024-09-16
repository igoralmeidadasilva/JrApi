using JrApi.Domain.Core.Abstractions;

namespace JrApi.Domain.Core.Interfaces.Repositories.Persistence;

public interface IPersistenceRepository<TEntity> where TEntity : Entity<TEntity>
{
    Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
