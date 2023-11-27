using MinimalRichDomain.SourceGenerators;

namespace VGSS.Domain;
[GenerateId]
public class User : Entity<UserId>
{
    public string Username { get; }
    public IReadOnlyCollection<BlogPost> BlogPosts => _blogPosts.AsReadOnly();

    private readonly List<BlogPost> _blogPosts;

    private User(UserId userId, string username) : base(userId)
    {
        Username = username;
        _blogPosts = new List<BlogPost>();
    }

    public static User New(string username) => new(UserId.New(), username);

    public static User Existing(UserId userId, string username) => new (userId, username);

    public BlogPost PostNewBlogPost(string title, string content)
    {
        var blogPost = BlogPost.New(Key, title, content);

        _blogPosts.Add(blogPost);

        RaiseDomainEvent(new NewBlogPostPostedEvent(Key, blogPost.Title, blogPost.Content, blogPost.PostedAt));
        return blogPost;
    }
}