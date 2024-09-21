using JrApi.Domain.Core.Abstractions;
using JrApi.Domain.Core.Interfaces.Repositories.Persistence;
using JrApi.Infrastructure.Context;

namespace JrApi.Infrastructure.Repositories.Persistence;

public abstract class BasePersistenceRepository<TEntity> : IPersistenceRepository<TEntity> where TEntity : Entity<TEntity>
{
    protected readonly ApplicationContext _context;

    protected BasePersistenceRepository(ApplicationContext context)
    {
        _context = context;
    }

    public void Insert(TEntity entity)
        => _context.Set<TEntity>().Add(entity);
    

    public void InsertRange(IEnumerable<TEntity> entities)
        => _context.Set<TEntity>().AddRange(entities);

    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    public void Delete(Guid id)
    {
        var item = _context.Set<TEntity>().Find(id);
        if (item != null)
        {
            _context.Set<TEntity>().Remove(item);
        }
    }
}
