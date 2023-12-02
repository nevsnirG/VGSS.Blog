using MediatR;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.Ports;
using VGSS.Domain.BloggerAggregate.Events;
using VGSS.Domain.BloggerAggregate.ValueObjects;

namespace VGSS.Application;
public static class NewBlogPost
{
    public sealed record class CreateNewBlogPostCommand(BloggerId BloggerId, string Title, string Content) : IRequest<BlogPost>;

    internal sealed class CreateNewBlogPostCommandHandler : IRequestHandler<CreateNewBlogPostCommand, BlogPost>
    {
        private readonly IGetBlogger _getUser;

        public CreateNewBlogPostCommandHandler(IGetBlogger getUser)
        {
            _getUser = getUser;
        }

        public async Task<BlogPost> Handle(CreateNewBlogPostCommand request, CancellationToken cancellationToken)
        {
            var user = await _getUser.GetByBloggerId(request.BloggerId);
            var title = new Title(request.Title);
            var content = new Content(request.Content);
            return user.PostNewBlogPost(title, content);
        }
    }

    internal sealed class NewBlogPostPostedEventHandler : INotificationHandler<NewBlogPostPostedEvent>
    {
        public Task Handle(NewBlogPostPostedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"A new blog post titled '{notification.Title}' was created!");
            return Task.CompletedTask;
        }
    }
}
