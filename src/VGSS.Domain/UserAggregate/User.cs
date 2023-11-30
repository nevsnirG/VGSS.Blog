using MinimalRichDomain.SourceGenerators;
using VGSS.Domain.BlogAggregate;
using VGSS.Domain.BlogAggregate.Events;

namespace VGSS.Domain.UserAggregate;
[GenerateId]
public class User : Entity<UserId>
{
    public Username Username { get; }
    public IReadOnlyCollection<BlogPost> BlogPosts => _blogPosts.AsReadOnly();

    private readonly List<BlogPost> _blogPosts;

    private User(UserId userId, Username username) : base(userId)
    {
        Username = username;
        _blogPosts = new List<BlogPost>();
    }

    public static User New(Username username) => new(UserId.New(), username);

    public static User Existing(UserId userId, Username username) => new(userId, username);

    public BlogPost PostNewBlogPost(string title, string content)
    {
        var blogPost = BlogPost.New(Id, title, content);

        _blogPosts.Add(blogPost);

        RaiseDomainEvent(new NewBlogPostPostedEvent(Id, blogPost.Title, blogPost.Content, blogPost.PostedAt));
        return blogPost;
    }
}