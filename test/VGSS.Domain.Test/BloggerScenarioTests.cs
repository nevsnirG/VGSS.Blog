using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BloggerAggregate.Events;
using VGSS.Domain.BloggerAggregate.ValueObjects;
using VGSS.Domain.BlogPostAggregate.Events;
using VGSS.Domain.BlogPostAggregate.ValueObjects;
using VGSS.TestCommon;
using static VGSS.TestCommon.ValidationHelper;

namespace VGSS.Domain.Test;
public class BloggerScenarioFixture : ScenarioFixtureBase
{
    public Blogger? Blogger { get; set; }
    public List<BlogPost> BlogPosts { get; private set; } = [];
}

[TestCaseOrderer("VGSS.TestCommon.PriorityOrderer", "VGSS.TestCommon")]
public class BloggerScenarioTests(BloggerScenarioFixture fixture) : IClassFixture<BloggerScenarioFixture>
{
    [Fact(DisplayName = "100 Register new user"), TestPriority(100)]
    public void RegisterNewBlogger()
    {
        var username = new Username("test username");

        var blogger = Blogger.Register(username);

        blogger.Should().NotBeNull();
        blogger.Username.Should().NotBeNull();
        blogger.Username.Value.Should().Be("test username");
        blogger.CurrentVersion.Should().Be(1);
        blogger.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<BloggerRegisteredEvent>()
            .Which.Should().BeEquivalentTo(
                new BloggerRegisteredEvent(blogger.Id, username, DateTimeOffset.UtcNow, 1),
                DefaultEquivalencyAssertionOptions<BloggerRegisteredEvent>()
            );

        ValidateRehydration<Blogger>(blogger.Id, blogger.DomainEvents);

        fixture.Blogger = blogger;
        fixture.Persist(100);
    }

    [Fact(DisplayName = "200 After registering, post a new blogpost"), TestPriority(200)]
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
        blogPost.CurrentVersion.Should().Be(1);
        blogPost.DomainEvents.Should().ContainSingle("because the blog has just been posted")
            .Which.Should().BeOfType<NewBlogPostPostedEvent>()
            .Which.Should().BeEquivalentTo(
                new NewBlogPostPostedEvent(blogPost.Id, blogger.Id, title, content, DateTimeOffset.UtcNow, 1),
                DefaultEquivalencyAssertionOptions<NewBlogPostPostedEvent>()
            );

        ValidateRehydration<BlogPost>(blogPost.Id, blogPost.DomainEvents);

        fixture.BlogPosts.Add(blogPost);
        fixture.Persist(200);
    }

    [Fact(DisplayName = "300 After posting a new blog post, view the posted blog post"), TestPriority(300)]
    public void AfterPosting_ViewsBlogPost()
    {
        var blogger = fixture.Blogger!;
        var blogPost = fixture.BlogPosts.Single();

        blogPost.View(blogger.Id);

        blogPost.Views.Should().Be(1);
        blogPost.CurrentVersion.Should().Be(2);
        blogPost.DomainEvents.Should().HaveCount(2);
        blogPost.DomainEvents.Last()
            .Should().BeOfType<BlogPostViewedEvent>()
            .Which.Should().BeEquivalentTo(
                new BlogPostViewedEvent(blogPost.Id, blogger.Id, DateTimeOffset.UtcNow, 2),
                DefaultEquivalencyAssertionOptions<BlogPostViewedEvent>()
            );

        ValidateRehydration<BlogPost>(blogPost.Id, blogPost.DomainEvents);
        fixture.Persist(300);
    }

    [Fact(DisplayName = "400 After viewing the new blogpost, the registered user edits the blogpost"), TestPriority(300)]
    public void AfterViewing_EditsBlogPost()
    {
        var blogger = fixture.Blogger!;
        var blogPost = fixture.BlogPosts.Single();

        var oldTitle = blogPost.Title;
        var newTitle = new Title("new title");
        var newContent = new Content("new content");
        blogPost.Edit(blogger.Id, newTitle, newContent);

        blogPost.Views.Should().Be(1);
        blogPost.Title.Should().NotBeNull();
        blogPost.Title.Value.Should().Be("new title");
        blogPost.Content.Should().NotBeNull();
        blogPost.Content.Value.Should().Be("new content");
        blogPost.CurrentVersion.Should().Be(3);
        blogPost.DomainEvents.Should().HaveCount(3);
        blogPost.DomainEvents.Last()
            .Should().BeOfType<BlogPostEditedEvent>()
            .Which.Should().BeEquivalentTo(
                new BlogPostEditedEvent(blogPost.Id, blogger.Id, DateTimeOffset.UtcNow, oldTitle, newTitle, newContent, 3),
                DefaultEquivalencyAssertionOptions<BlogPostEditedEvent>()
            );

        ValidateRehydration<BlogPost>(blogPost.Id, blogPost.DomainEvents);
        fixture.Persist(400);
    }
}