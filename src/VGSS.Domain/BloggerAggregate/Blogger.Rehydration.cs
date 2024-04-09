using VGSS.Domain.BloggerAggregate.Events;

namespace VGSS.Domain.BloggerAggregate;
public partial class Blogger
{
    protected override void ValidateState()
    {
        if (Username is null)
            throw new InvalidOperationException("Blogger is in a corrupt state. Username is missing.");
    }

    protected override void Apply(MinimalRichDomain.IDomainEvent @event)
    {
        ApplyEvent((dynamic)@event);
    }

    private void ApplyEvent(BloggerRegisteredEvent @event)
    {
        Username = @event.Username;
    }
}
