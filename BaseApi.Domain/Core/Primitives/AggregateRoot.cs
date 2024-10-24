using BaseApi.Domain.Core.Events;

namespace BaseApi.Domain.Core.Primitives;

public abstract class AggregateRoot<TId,TValue> : 
    Entity<TId, TValue> where TId : StronglyTypedId<TValue>
{
    protected AggregateRoot(TId id) : base(id)
    {
    }

    protected AggregateRoot()
    {
    }

    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}