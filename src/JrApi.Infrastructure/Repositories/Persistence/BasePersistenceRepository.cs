using JrApi.Domain.Core.Interfaces.Repositories.Persistence;

namespace JrApi.Infrastructure.Repositories.Persistence;

public abstract class BasePersistenceRepository<TEntity> : IPersistenceRepository<TEntity> where TEntity : Entity<TEntity>
{
    protected readonly ApplicationContext Context;

    protected BasePersistenceRepository(ApplicationContext context)
    {
        Context = context;
    }

    public virtual void Insert(TEntity entity)
        => Context.Set<TEntity>().Add(entity);
    

    public virtual void InsertRange(IEnumerable<TEntity> entities)
        => Context.Set<TEntity>().AddRange(entities);

    public virtual void Update(TEntity entity)
        => Context.Set<TEntity>().Update(entity);

    public virtual void Delete(Guid id)
    {
        var item = Context.Set<TEntity>().Find(id);
        if (item != null)
        {
            Context.Set<TEntity>().Remove(item);
        }
    }
}
