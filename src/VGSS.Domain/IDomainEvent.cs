using MediatR;

namespace VGSS.Domain;
public interface IDomainEvent : MinimalRichDomain.IDomainEvent, INotification { }