using VGSS.Domain;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BloggerAggregate.Events;
using VGSS.Domain.BloggerAggregate.ValueObjects;
using VGSS.Domain.BlogPostAggregate.Events;
using VGSS.Domain.BlogPostAggregate.ValueObjects;

namespace VGSS.MockPersistence;
public static class SeedData
{
    private static readonly Dictionary<BloggerId, List<IDomainEvent>> _bloggers = [];
    private static readonly Dictionary<BlogPostId, List<IDomainEvent>> _blogPosts = [];

    public static List<Blogger> Bloggers { get; } = [];
    public static List<BlogPost> BlogPosts { get; } = [];

    static SeedData()
    {
        for (var i = 0; i < 10; i++)
        {
            var bloggerId = BloggerId.New();
            var bloggerRegisteredEvent = new BloggerRegisteredEvent(bloggerId, Username.Create("Henk" + i)!, DateTimeOffset.UtcNow, 1);
            _bloggers.Add(bloggerId, [bloggerRegisteredEvent]);

            var blogPostId = BlogPostId.New();
            var newBlogPostPostedEvent = new NewBlogPostPostedEvent(blogPostId, bloggerId, Title.Create("Title" + i)!, Content.Create("Content" + i)!, DateTimeOffset.UtcNow, 1);
            _blogPosts.Add(blogPostId, [newBlogPostPostedEvent]);

            var user = new Blogger(bloggerId, _bloggers[bloggerId]);
            var blogPost = new BlogPost(blogPostId, _blogPosts[blogPostId]);

            Bloggers.Add(user);
            BlogPosts.Add(blogPost);
        }
    }
}
