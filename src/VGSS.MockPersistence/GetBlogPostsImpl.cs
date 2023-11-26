using VGSS.Domain;
using VGSS.Domain.Ports;

namespace VGSS.MockPersistence;
internal sealed class GetBlogPostsImpl : IGetBlogPosts
{
    private readonly List<BlogPost> _blogPosts;

    public GetBlogPostsImpl()
    {
        static BlogPost create()
        {
            var user = User.New("username");
            return user.PostNewBlogPost("This is a title", "this is content");
        };
        _blogPosts = new List<BlogPost>
        {
            create()
        };
    }

    public Task<BlogPost> GetBlogPostById(BlogPostId blogPostId)
    {
        return Task.FromResult(
            _blogPosts.Single(x => x.Key.Value == blogPostId.Value)
            );
    }

    public Task<IReadOnlyCollection<BlogPost>> GetBlogPosts()
    {
        return Task.FromResult(
            (IReadOnlyCollection<BlogPost>)_blogPosts.AsReadOnly()
            );
    }
}
