using MediatR;
using VGSS.Domain;

namespace VGSS.Application.NewBlogPost;
internal sealed class NewBlogPostPostedEventHandler : INotificationHandler<NewBlogPostPostedEvent>
{
    public Task Handle(NewBlogPostPostedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"A new blog post titled '{notification.Title}' was created!");
        return Task.CompletedTask;
    }
}
