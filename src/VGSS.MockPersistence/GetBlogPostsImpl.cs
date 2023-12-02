using VGSS.Domain.Ports;
using VGSS.Domain.BloggerAggregate;

namespace VGSS.MockPersistence;
internal sealed class GetBlogPostsImpl : IGetBlogPosts
{
    public Task<BlogPost> GetBlogPostById(BlogPostId blogPostId)
    {
        return Task.FromResult(
            SeedData.BlogPosts.Single(x => x.Id == blogPostId)
            );
    }

    public Task<IReadOnlyCollection<BlogPost>> GetBlogPosts()
    {
        return Task.FromResult(
            (IReadOnlyCollection<BlogPost>)SeedData.BlogPosts.AsReadOnly()
            );
    }
}
