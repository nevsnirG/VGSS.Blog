using VGSS.Domain.UserAggregate;
using MinimalRichDomain.SourceGenerators;

namespace VGSS.Domain.BlogAggregate;

[GenerateId]
public class BlogPost : Entity<BlogPostId>
{
    public UserId PostedBy { get; }
    public string Title { get; }
    public string Content { get; }
    public DateTimeOffset PostedAt { get; }
    public uint Views { get; private set; }

    private BlogPost(BlogPostId key,
                     UserId postedBy,
                     string title,
                     string content,
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

    internal static BlogPost New(UserId postedBy, string title, string content)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException(nameof(title), "A blog post must have a title.");
        if (string.IsNullOrEmpty(content))
            throw new ArgumentNullException(nameof(content), "A blog post must have content.");

        return new(BlogPostId.New(), postedBy, title, content, 0, DateTimeOffset.UtcNow);
    }

    public static BlogPost Existing(BlogPostId blogPostId, UserId postedBy, string title, string content)
    {
        return new BlogPost(blogPostId, postedBy, title, content, 0, DateTimeOffset.UtcNow);
    }

    public void View(/*UserId viewedBy*/)
    {
        Views++;
        //RaiseDomainEvent(new BlogPostViewed(Key, viewedBy, DateTimeOffset.UtcNow));
    }
}