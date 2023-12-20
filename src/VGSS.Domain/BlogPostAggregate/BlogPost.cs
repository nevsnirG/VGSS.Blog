using MinimalRichDomain;
using VGSS.Domain.BlogPostAggregate.Events;
using VGSS.Domain.BlogPostAggregate.ValueObjects;

namespace VGSS.Domain.BloggerAggregate;
[GenerateId]
public partial class BlogPost : AggregateRoot<BlogPostId>
{
    public BloggerId PostedBy { get; private set; }
    public Title Title { get; private set; }
    public Content Content { get; private set; }
    public DateTimeOffset PostedAt { get; private set; }
    public uint Views { get; private set; }

#pragma warning disable CS8618 // Rehydration validates invariants are correct.
    public BlogPost(BlogPostId id, IReadOnlyCollection<MinimalRichDomain.IDomainEvent> domainEvents) : base(id, domainEvents) { }

    private BlogPost() : base(BlogPostId.New()) { }
#pragma warning restore CS8618

    public static BlogPost New(BloggerId postedBy, Title title, Content content)
    {
        var blogPost = new BlogPost();
        var domainEvent = new NewBlogPostPostedEvent(blogPost.Id, postedBy, title, content, DateTimeOffset.UtcNow, blogPost.NextVersion);
        blogPost.RaiseAndApplyDomainEvent(domainEvent);
        return blogPost;
    }

    public void View(BloggerId viewedBy)
    {
        RaiseAndApplyDomainEvent(new BlogPostViewedEvent(Id, viewedBy, DateTimeOffset.UtcNow, NextVersion));
    }
}