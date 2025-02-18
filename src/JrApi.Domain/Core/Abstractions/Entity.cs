namespace JrApi.Domain.Core.Abstractions;

public abstract class Entity<T>
{
    public Guid Id { get; private init; }
    public DateTime CreatedOnUtc { get; private init; }

    protected Entity() { } // ORM

    protected Entity(Guid id, DateTime createdOnUtc)
    {
        Id = id;
        CreatedOnUtc = createdOnUtc;
    }
}