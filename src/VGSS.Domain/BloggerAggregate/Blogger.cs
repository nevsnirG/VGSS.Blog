using MinimalDomainEvents.Contract;
using MinimalDomainEvents.Core;
using MinimalRichDomain.SourceGenerators;
using VGSS.Domain.BloggerAggregate.Events;
using VGSS.Domain.BloggerAggregate.ValueObjects;

namespace VGSS.Domain.BloggerAggregate;
[GenerateId]
public partial class Blogger : Entity<BloggerId>
{
    public Username Username { get; private set; }
    public IReadOnlyCollection<BlogPost> BlogPosts => _blogPosts.AsReadOnly();

    private readonly List<BlogPost> _blogPosts = new();

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
        DomainEventTracker.RaiseDomainEvent(new BloggerRegisteredEvent(blogger.Id, username));
        return blogger;
    }

    public BlogPost PostNewBlogPost(Title title, Content content)
    {
        var blogPost = BlogPost.New(Id, title, content);

        _blogPosts.Add(blogPost);

        RaiseDomainEvent(new NewBlogPostPostedEvent(blogPost.Id, Id, blogPost.Title, blogPost.Content, blogPost.PostedAt));
        return blogPost;
    }
}