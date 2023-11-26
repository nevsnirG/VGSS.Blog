namespace VGSS.Domain.Ports;
public interface IGetBlogPosts
{
    Task<IReadOnlyCollection<BlogPost>> GetBlogPosts();
    Task<BlogPost> GetBlogPostById(BlogPostId blogPostId);
}
