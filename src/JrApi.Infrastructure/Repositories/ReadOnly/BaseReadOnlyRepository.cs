using JrApi.Domain.Core.Abstractions;
using JrApi.Domain.Core.Interfaces.Repositories.ReadOnly;
using JrApi.Infrastructure.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Dapper.SqlMapper;

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
        string query = $"SELECT * FROM {_tableName}";
        using IDbConnection db = new SqliteConnection(_connectionString);
        return await db.QueryAsync<TEntity>(query);
    }
    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = $"SELECT * FROM {_tableName} WHERE id = @Id";
        using IDbConnection db = new SqliteConnection(_connectionString);
        return (await db.QuerySingleOrDefaultAsync<TEntity>(query, new { Id = id }))!;
    }
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = $"SELECT 1 FROM {_tableName} WHERE id = @Id";
        using IDbConnection db = new SqliteConnection(_connectionString);
        return (await db.QuerySingleOrDefaultAsync<bool>(query, new { Id = id }))!;
    }
    public async Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> func, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_context.Set<TEntity>().AsNoTracking().Where(func).AsEnumerable()); 
    }
}
