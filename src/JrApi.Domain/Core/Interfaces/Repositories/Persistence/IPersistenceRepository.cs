using JrApi.Domain.Core.Abstractions;

namespace JrApi.Domain.Core.Interfaces.Repositories.Persistence;

public interface IPersistenceRepository<TEntity> where TEntity : Entity<TEntity>
{
    void Insert(TEntity entity);
    void InsertRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Delete(Guid id);
}
