using MinimalDomainEvents.Contract;
using MinimalDomainEvents.Core;

namespace VGSS.Domain;
public abstract class Entity<TId>
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public TId Id { get; }

    private readonly List<IDomainEvent> _domainEvents;

    protected Entity(TId id)
    {
        Id = id;
        _domainEvents = new();
    }

    protected Entity(TId id, IReadOnlyCollection<IDomainEvent> domainEvents)
    {
        Id = id;
        Rehydrate(domainEvents);
        _domainEvents = new(domainEvents);
    }

    protected virtual void Rehydrate(IReadOnlyCollection<IDomainEvent> domainEvents)
    {
        foreach (var @event in domainEvents)
        {
            Apply(@event);
        }

        ValidateRehydration();
    }

    private void Apply(IDomainEvent @event)
    {
        var eventType = @event.GetType();
        var interfaceType = typeof(IApplyEvent<>).MakeGenericType(eventType);

        var applyMethod = GetType().GetInterfaceMap(interfaceType).TargetMethods
            .FirstOrDefault(m => m.Name.EndsWith(nameof(IApplyEvent<IDomainEvent>.Apply)));

        applyMethod?.Invoke(this, new object[] { @event });
    }

    protected abstract void ValidateRehydration();

    protected virtual void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        DomainEventTracker.RaiseDomainEvent(domainEvent);
        _domainEvents.Add(domainEvent);
    }
}