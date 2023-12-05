using MediatR;

namespace VGSS.Domain;
public interface IDomainEvent : MinimalDomainEvents.Contract.IDomainEvent, INotification
{
    int Version { get; }
}