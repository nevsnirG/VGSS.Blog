namespace VGSS.Domain;
//TODO - Extract to shared kernel, along with Entity and ValueObject.
internal interface IApplyEvent<TEvent> where TEvent : IDomainEvent
{
    void Apply(TEvent @event);
}