using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.SharedKernel;
using System.Data;
using System.Linq.Expressions;

namespace JrApi.Infrastructure.Repositories.ReadOnly;

public abstract class BaseReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : Entity<TEntity>
{
    protected readonly ApplicationContext Context;
    protected readonly string ConnectionString;
    protected readonly string TableName;

    protected BaseReadOnlyRepository(ApplicationContext context)
    {
        Context = context;
        ConnectionString = Context.Database.GetDbConnection().ConnectionString;

        var entityType = typeof(TEntity);
        var modelEntityType = context.Model.FindEntityType(entityType);
        TableName = modelEntityType!.GetSchemaQualifiedTableName()!;
    }

    public virtual async Task<PagedList<TEntity>> GetPagedAsync<TKey>(
        int pageNumber, 
        int pageSize, 
        Expression<Func<TEntity, TKey>> orderBy,
        CancellationToken cancellationToken = default)
    {
        List<TEntity> response = await Context.Set<TEntity>()
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(orderBy)
            .ToListAsync(cancellationToken);
        int totalCount = await Context.Set<TEntity>().CountAsync(cancellationToken);
        return new PagedList<TEntity>(response, totalCount, pageNumber, pageSize);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
        
    public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => (await Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken))!;

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => await Context.Set<TEntity>().AsNoTracking().AnyAsync(x => x.Id == id, cancellationToken);

    public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func, CancellationToken cancellationToken = default)
        => (await Context.Set<TEntity>().AsNoTracking().Where(func).FirstOrDefaultAsync(cancellationToken))!;

    public virtual async Task<IEnumerable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> func, CancellationToken cancellationToken = default)
        => await Context.Set<TEntity>().AsNoTracking().Where(func).ToListAsync(cancellationToken);
}