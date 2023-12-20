using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BloggerAggregate.ValueObjects;
using VGSS.Domain.BlogPostAggregate.ValueObjects;
using VGSS.TestCommon;

namespace VGSS.Domain.Test;
public class BloggerAggregateStateFixture
{
    public Blogger? Blogger { get; set; }
    public List<BlogPost> BlogPosts { get; } = [];
}

[TestCaseOrderer("VGSS.TestCommon.PriorityOrderer", "VGSS.TestCommon")]
public class BloggerScenarioTests(BloggerAggregateStateFixture fixture) : IClassFixture<BloggerAggregateStateFixture>
{
    [Fact, TestPriority(100)]
    public void RegisterNewBlogger()
    {
        var username = new Username("test username");

        var blogger = Blogger.Register(username);

        blogger.Should().NotBeNull();
        blogger.Username.Should().NotBeNull();
        blogger.Username.Value.Should().Be("test username");

        fixture.Blogger = blogger;
    }

    [Fact, TestPriority(200)]
    public void AfterRegistering_PostsNewBlogPost()
    {
        var blogger = fixture.Blogger!;

        var title = new Title("test title");
        var content = new Content("test content");

        var blogPost = blogger.PostNewBlogPost(title, content);

        blogPost.Should().NotBeNull();
        blogPost.Title.Should().NotBeNull();
        blogPost.Title.Value.Should().Be("test title");
        blogPost.Content.Should().NotBeNull();
        blogPost.Content.Value.Should().Be("test content");

        fixture.BlogPosts.Add(blogPost);
    }

    [Fact, TestPriority(300)]
    public void AfterPosting_ViewsBlogPost()
    {
        var blogger = fixture.Blogger!;
        var blogPost = fixture.BlogPosts.Single();

        blogPost.View(blogger.Id);

        blogPost.Views.Should().Be(1);
        blogPost.CurrentVersion.Should().Be(2);
    }
}