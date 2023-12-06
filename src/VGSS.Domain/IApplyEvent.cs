namespace VGSS.Domain;
internal interface IApplyEvent<TEvent> where TEvent : IDomainEvent
{
    void Apply(TEvent @event);
}