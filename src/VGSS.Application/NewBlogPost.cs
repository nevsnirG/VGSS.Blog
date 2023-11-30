using MediatR;
using VGSS.Domain.BlogAggregate;
using VGSS.Domain.BlogAggregate.Events;
using VGSS.Domain.Ports;
using VGSS.Domain.UserAggregate;

namespace VGSS.Application;
public static class NewBlogPost
{
    public sealed record class CreateNewBlogPostCommand(UserId UserId, string Title, string Content) : IRequest<BlogPost>;

    internal sealed class CreateNewBlogPostCommandHandler : IRequestHandler<CreateNewBlogPostCommand, BlogPost>
    {
        private readonly IGetUser _getUser;

        public CreateNewBlogPostCommandHandler(IGetUser getUser)
        {
            _getUser = getUser;
        }

        public async Task<BlogPost> Handle(CreateNewBlogPostCommand request, CancellationToken cancellationToken)
        {
            var user = await _getUser.GetByUserId(request.UserId);
            return user.PostNewBlogPost(request.Title, request.Content);
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
