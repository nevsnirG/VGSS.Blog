using MediatR;
using MinimalRichDomain;
using VGSS.Domain.BloggerAggregate.Events;
using VGSS.Domain.BloggerAggregate.ValueObjects;
using VGSS.Domain.BlogPostAggregate.ValueObjects;

namespace VGSS.Domain.BloggerAggregate;
[GenerateId]
public partial class Blogger : AggregateRoot<BloggerId>
{
    public Username Username { get; private set; }

#pragma warning disable CS8618 // Rehydration validates invariants are correct.
    public Blogger(BloggerId id, IReadOnlyCollection<MinimalRichDomain.IDomainEvent> domainEvents) : base(id, domainEvents) { }

    private Blogger() : base(BloggerId.New()) { }
#pragma warning restore CS8618

    public static Blogger Register(Username username)
    {
        var blogger = new Blogger();
        blogger.RaiseAndApplyDomainEvent(new BloggerRegisteredEvent(blogger.Id, username, DateTimeOffset.UtcNow, 1));
        return blogger;
    }

    public BlogPost PostNewBlogPost(Title title, Content content)
    {
        var blogPost = BlogPost.New(Id, title, content);
        return blogPost;
    }
}