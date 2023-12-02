using MinimalDomainEvents.Contract;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BloggerAggregate.Events;

namespace VGSS.MockPersistence;
public static class SeedData
{
    private static readonly Dictionary<BloggerId, List<IDomainEvent>> _bloggers = new();
    private static readonly Dictionary<BlogPostId, List<IDomainEvent>> _blogPosts = new();

    public static List<Blogger> Bloggers { get; } = new();
    public static List<BlogPost> BlogPosts { get; } = new();

    static SeedData()
    {
        for (var i = 0; i < 10; i++)
        {
            var bloggerId = BloggerId.New();
            var bloggerRegisteredEvent = new BloggerRegisteredEvent(bloggerId, new Username("Henk" + i));
            _bloggers.Add(bloggerId, new List<IDomainEvent> { bloggerRegisteredEvent });

            var blogPostId = BlogPostId.New();
            var newBlogPostPostedEvent = new NewBlogPostPostedEvent(blogPostId, bloggerId, new Title("Title" + i), new Content("Content" + i), DateTimeOffset.UtcNow);
            _blogPosts.Add(blogPostId, new List<IDomainEvent> { newBlogPostPostedEvent });


            var user = new Blogger(bloggerId, _bloggers[bloggerId]);
            var blogPost = new BlogPost(blogPostId, _blogPosts[blogPostId]);

            Bloggers.Add(user);
            BlogPosts.Add(blogPost);
        }
    }
}
