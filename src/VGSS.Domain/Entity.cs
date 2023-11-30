using MinimalDomainEvents.Contract;
using MinimalDomainEvents.Core;

namespace VGSS.Domain;
public abstract class Entity<TId>
{
    public TId Id { get; }

    protected Entity(TId id)
    {
        Id = id;
    }

    protected virtual void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        DomainEventTracker.RaiseDomainEvent(domainEvent);
    }
}