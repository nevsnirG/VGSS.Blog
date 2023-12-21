using VGSS.Domain.Ports;
using VGSS.Domain.BloggerAggregate;

namespace VGSS.MockPersistence;
internal sealed class BlogPostRepositoryMock : IBlogPostRepository
{
    public Task<IReadOnlyCollection<BlogPost>> GetAll()
    {
        return Task.FromResult(
            (IReadOnlyCollection<BlogPost>)SeedData.BlogPosts.AsReadOnly()
            );
    }

    public Task<BlogPost?> GetById(BlogPostId blogPostId)
    {
        return Task.FromResult(
            SeedData.BlogPosts.SingleOrDefault(x => x.Id == blogPostId)
            );
    }

    public Task Save(BlogPost blogPost)
    {
        SeedData.BlogPosts.Add(blogPost);
        return Task.CompletedTask;
    }
}
