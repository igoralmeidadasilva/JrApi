using JrApi.Domain.Core.Abstractions;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace JrApi.Infrastructure.Repositories.ReadOnly;

public abstract class BaseReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : Entity<TEntity>
{
    protected readonly ApplicationContext _context;
    protected readonly string _connectionString;
    protected readonly string _tableName;

    protected BaseReadOnlyRepository(ApplicationContext context)
    {
        _context = context;
        _connectionString = _context.Database.GetDbConnection().ConnectionString;

        var entityType = typeof(TEntity);
        var modelEntityType = context.Model.FindEntityType(entityType);
        _tableName = modelEntityType!.GetSchemaQualifiedTableName()!;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
    }
    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return (await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken))!;
    }
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AsNoTracking().AnyAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> func, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_context.Set<TEntity>().AsNoTracking().Where(func).AsEnumerable()); 
    }
}
