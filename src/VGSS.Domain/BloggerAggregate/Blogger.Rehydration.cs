using VGSS.Domain.BloggerAggregate.Events;

namespace VGSS.Domain.BloggerAggregate;
public partial class Blogger : IApplyEvent<BloggerRegisteredEvent>
{
    protected override void ValidateRehydration()
    {
        if (Username is null)
            throw new InvalidOperationException("Blogger rehydrated in corrupt state. Username is missing.");
    }

    void IApplyEvent<BloggerRegisteredEvent>.Apply(BloggerRegisteredEvent @event)
    {
        Username = @event.Username;
    }
}
