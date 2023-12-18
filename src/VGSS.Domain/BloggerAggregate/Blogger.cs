using MinimalRichDomain;
using VGSS.Domain.BloggerAggregate.Events;
using VGSS.Domain.BloggerAggregate.ValueObjects;

namespace VGSS.Domain.BloggerAggregate;
[GenerateId]
public partial class Blogger : AggregateRoot<BloggerId>
{
    public Username Username { get; private set; }

#pragma warning disable CS8618 // Rehydration validates invariants are correct.
    public Blogger(BloggerId id, IReadOnlyCollection<IDomainEvent> domainEvents) : base(id, domainEvents) { }

    private Blogger() : base(BloggerId.New()) { }
#pragma warning restore CS8618

    public static Blogger New(Username username)
    {
        var blogger = new Blogger();
        blogger.RaiseAndApplyDomainEvent(new BloggerRegisteredEvent(blogger.Id, username, 1));
        return blogger;
    }
}