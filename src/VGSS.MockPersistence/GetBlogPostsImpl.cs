using VGSS.Domain;
using VGSS.Domain.Ports;

namespace VGSS.MockPersistence;
internal sealed class GetBlogPostsImpl : IGetBlogPosts
{
    public Task<BlogPost> GetBlogPostById(BlogPostId blogPostId)
    {
        return Task.FromResult(
            SeedData.BlogPosts.Single(x => x.Key == blogPostId)
            );
    }

    public Task<IReadOnlyCollection<BlogPost>> GetBlogPosts()
    {
        return Task.FromResult(
            (IReadOnlyCollection<BlogPost>)SeedData.BlogPosts.AsReadOnly()
            );
    }
}
