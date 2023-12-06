using MinimalRichDomain.SourceGenerators;
using VGSS.Domain.BlogPostAggregate.Events;
using VGSS.Domain.BlogPostAggregate.ValueObjects;

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

    public static BlogPost New(BloggerId postedBy, Title title, Content content)
    {
        var blogPost = new BlogPost(BlogPostId.New(), postedBy, title, content, 0, DateTimeOffset.UtcNow);
        blogPost.RaiseDomainEvent(new NewBlogPostPostedEvent(blogPost.Id, postedBy, blogPost.Title, blogPost.Content, blogPost.PostedAt, 1));
        return blogPost;
    }

    public void View(BloggerId viewedBy)
    {
        RaiseDomainEvent(new BlogPostViewedEvent(Id, viewedBy, DateTimeOffset.UtcNow, NextVersion));
    }
}