using VGSS.Domain.BloggerAggregate;

namespace VGSS.Domain.Ports;
public interface IBlogPostRepository
{
    Task Save(BlogPost blogPost);
    Task<IReadOnlyCollection<BlogPost>> GetAll();
    Task<BlogPost> GetById(BlogPostId blogPostId);
}
