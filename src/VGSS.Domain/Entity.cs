using MinimalDomainEvents.Contract;
using MinimalDomainEvents.Core;

namespace VGSS.Domain;
public abstract class Entity<TKey>
{
    public TKey Key { get; }

    protected Entity(TKey key)
    {
        Key = key;
    }

    protected virtual void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        DomainEventTracker.RaiseDomainEvent(domainEvent);
    }
}