using MinimalDomainEvents.Core;
using MinimalRichDomain;
using VGSS.Domain.BloggerAggregate.Events;
using VGSS.Domain.BloggerAggregate.ValueObjects;

namespace VGSS.Domain.BloggerAggregate;
[GenerateId]
public partial class Blogger : Entity<BloggerId>
{
    public Username Username { get; private set; }


#pragma warning disable CS8618 // Rehydration validates invariants are correct.
    public Blogger(BloggerId id, IReadOnlyCollection<IDomainEvent> domainEvents) : base(id, domainEvents) { }
#pragma warning restore CS8618

    private Blogger(BloggerId BloggerId, Username username) : base(BloggerId)
    {
        Username = username;
    }

    public static Blogger New(Username username)
    {
        var blogger = new Blogger(BloggerId.New(), username);
        DomainEventTracker.RaiseDomainEvent(new BloggerRegisteredEvent(blogger.Id, username, 1));
        return blogger;
    }
}