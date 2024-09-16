using JrApi.Domain.Core.Interfaces;

namespace JrApi.Domain.Core.Abstractions;

public abstract class AggregateRoot<T> : Entity<T>
{
    private readonly List<IDomainEvent> _events = [];
    public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

    public AggregateRoot(Guid id) : base(id)
    { }

    public AggregateRoot() : base()
    { }

    public void ClearDomainEvents() => _events.Clear();

    public void AddDomainEvent(IDomainEvent domainEvent) => _events.Add(domainEvent);
}