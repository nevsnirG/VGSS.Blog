using System.Text.Json;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BloggerAggregate.Events;
using VGSS.Domain.BloggerAggregate.ValueObjects;
using VGSS.Domain.BlogPostAggregate.Events;
using VGSS.Domain.BlogPostAggregate.ValueObjects;
using VGSS.TestCommon;
using static VGSS.TestCommon.ValidationHelper;

namespace VGSS.Domain.Test;
public class BloggerScenarioFixture
{
    public Blogger? Blogger { get; set; }
    public List<BlogPost> BlogPosts { get; private set; } = [];

    public void Persist(int priority)
    {
        var statesDirectory = GetStatesDirectory();
        Directory.CreateDirectory(statesDirectory);
        var fileName = Path.Combine(statesDirectory, $"{priority}.json");
        var stateAsJson = JsonSerializer.Serialize(this);
        File.WriteAllText(fileName, stateAsJson);
    }

    private string GetStatesDirectory()
    {
        var projectDir = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
        var statesDirectory = Path.Combine(projectDir, "States");
        return statesDirectory;
    }
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
                new
                {
                    blogPost.Id,
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
                new
                {
                    blogPost.Id,
                    ViewedBy = blogger.Id,
                    ViewedAt = DateTimeOffset.UtcNow,
                    Version = 2
                }, options => options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTimeOffset>()
            );

        ValidateRehydration<BlogPost>(blogPost.Id, blogPost.DomainEvents);
        fixture.Persist(300);
    }

    [Fact(DisplayName = "400 After viewing the new blogpost, the registered user edits the blogpost"), TestPriority(300)]
    public void AfterViewing_EditsBlogPost()
    {
        var blogger = fixture.Blogger!;
        var blogPost = fixture.BlogPosts.Single();

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
                new
                {
                    blogPost.Id,
                    EditedBy = blogger.Id,
                    EditedAt = DateTimeOffset.UtcNow,
                    NewTitle = newTitle,
                    NewContent = newContent,
                    Version = 3
                }, options => options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTimeOffset>()
            );

        ValidateRehydration<BlogPost>(blogPost.Id, blogPost.DomainEvents);
        fixture.Persist(400);
    }
}