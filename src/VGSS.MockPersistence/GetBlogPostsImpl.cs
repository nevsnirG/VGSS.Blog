using VGSS.Domain;
using VGSS.Domain.Ports;

namespace VGSS.MockPersistence;
internal sealed class GetBlogPostsImpl : IGetBlogPosts
{
    private readonly List<BlogPost> _blogPosts;

    public GetBlogPostsImpl(IGetUser getUser)
    {
        var getUserImpl = getUser as GetUserImpl;
        static BlogPost create(string username, GetUserImpl getUserImpl)
        {
            var user = User.New(username);
            getUserImpl.AddUser(user);
            return user.PostNewBlogPost("This is a title", "this is content");
        };

        _blogPosts = new List<BlogPost>
        {
            create("Ken", getUserImpl!),
            create("Henk", getUserImpl!),
            create("Klaas", getUserImpl!),
        };
    }

    public Task<BlogPost> GetBlogPostById(BlogPostId blogPostId)
    {
        return Task.FromResult(
            _blogPosts.Single(x => x.Key == blogPostId)
            );
    }

    public Task<IReadOnlyCollection<BlogPost>> GetBlogPosts()
    {
        return Task.FromResult(
            (IReadOnlyCollection<BlogPost>)_blogPosts.AsReadOnly()
            );
    }
}
