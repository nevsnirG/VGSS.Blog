using MediatR;
using VGSS.Domain.Entities.BlogPost;

namespace VGSS.Application.ViewBlogPost;
internal sealed class BlogPostViewedEventHandler : INotificationHandler<BlogPostViewed>
{
    public Task Handle(BlogPostViewed notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"User '{notification.ViewedBy.Value}' viewed your blog post!");
        return Task.CompletedTask;
    }
}
