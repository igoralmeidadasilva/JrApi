namespace JrApi.Domain.Core.Abstractions;

public abstract class Entity<T>
{
    public Guid Id { get; private init; }
    public DateTime CreatedOnUtc { get; private init; }

    protected Entity() 
    { 
        Id = Guid.NewGuid();
        CreatedOnUtc = DateTime.UtcNow;
    }
    
    public abstract T Update(T entity);
}