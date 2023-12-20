using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BloggerAggregate.Events;
using VGSS.Domain.BloggerAggregate.ValueObjects;
using VGSS.Domain.BlogPostAggregate.Events;
using VGSS.Domain.BlogPostAggregate.ValueObjects;
using VGSS.TestCommon;
using static VGSS.TestCommon.ValidationHelper;

namespace VGSS.Domain.Test;
public class BloggerAggregateStateFixture
{
    public Blogger? Blogger { get; set; }
    public List<BlogPost> BlogPosts { get; } = [];
}

[TestCaseOrderer("VGSS.TestCommon.PriorityOrderer", "VGSS.TestCommon")]
public class BloggerScenarioTests(BloggerAggregateStateFixture fixture) : IClassFixture<BloggerAggregateStateFixture>
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
                new
                {
                    BloggerId = blogger.Id,
                    Version = 1,
                    Username = username,
                    RegisteredAt = DateTimeOffset.UtcNow
                }, options => options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTimeOffset>()
            );

        ValidateRehydration<Blogger>(blogger.Id, blogger.DomainEvents);

        fixture.Blogger = blogger;
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
                new
                {
                    PostedBy = blogger.Id,
                    Content = content,
                    Title = title,
                    PostedAt = DateTimeOffset.UtcNow,
                    Version = 1
                },
                options => options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTimeOffset>()
            );

        ValidateRehydration<BlogPost>(blogPost.Id, blogPost.DomainEvents);

        fixture.BlogPosts.Add(blogPost);
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
        blogPost.DomainEvents.Last().Should().BeEquivalentTo(
                new
                {
                    BlogPostId = blogPost.Id,
                    ViewedBy = blogger.Id,
                    ViewedAt = DateTimeOffset.UtcNow,
                    Version = 2
                }, options => options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTimeOffset>()
            );

        ValidateRehydration<BlogPost>(blogPost.Id, blogPost.DomainEvents);
    }
}