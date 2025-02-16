using JrApi.Domain.Core.Abstractions;
using JrApi.SharedKernel;
using System.Linq.Expressions;

namespace JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;

public interface IReadOnlyRepository<TEntity> where TEntity : Entity<TEntity>
{
    Task<PagedList<TEntity>> GetPagedAsync<TKey>(
        int pageNumber, 
        int pageSize, 
        Expression<Func<TEntity, TKey>> orderBy, 
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> func, CancellationToken cancellationToken = default);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func, CancellationToken cancellationToken = default);
}