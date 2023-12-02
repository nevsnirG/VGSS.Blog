using MinimalDomainEvents.Contract;
using MinimalRichDomain.SourceGenerators;
using VGSS.Domain.BloggerAggregate.Events;
using VGSS.Domain.BloggerAggregate.ValueObjects;

namespace VGSS.Domain.BloggerAggregate;
[GenerateId]
public partial class BlogPost : Entity<BlogPostId>
{
    public BloggerId PostedBy { get; private set; }
    public Title Title { get; private set; }
    public Content Content { get; private set; }
    public DateTimeOffset PostedAt { get; private set; }
    public uint Views { get; private set; }

#pragma warning disable CS8618 // Rehydration validates invariants are correct.
    public BlogPost(BlogPostId id, IReadOnlyCollection<IDomainEvent> domainEvents) : base(id, domainEvents) { }
#pragma warning restore CS8618

    private BlogPost(BlogPostId key,
                     BloggerId postedBy,
                     Title title,
                     Content content,
                     uint views,
                     DateTimeOffset postedAt)
        : base(key)
    {
        PostedBy = postedBy;
        Title = title;
        Content = content;
        Views = views;
        PostedAt = postedAt;
    }

    internal static BlogPost New(BloggerId postedBy, Title title, Content content)
    {
        return new(BlogPostId.New(), postedBy, title, content, 0, DateTimeOffset.UtcNow);
    }

    public static BlogPost Existing(BlogPostId blogPostId, BloggerId postedBy, Title title, Content content)
    {
        return new BlogPost(blogPostId, postedBy, title, content, 0, DateTimeOffset.UtcNow);
    }

    public void View(BloggerId viewedBy)
    {
        Views++;
        RaiseDomainEvent(new BlogPostViewed(Id, viewedBy, DateTimeOffset.UtcNow));
    }
}