using MediatR;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BlogPostAggregate.Events;
using VGSS.Domain.BlogPostAggregate.ValueObjects;

namespace VGSS.Application;
public static class NewBlogPost
{
    public sealed record class CreateNewBlogPostCommand(BloggerId BloggerId, string Title, string Content) : IRequest<BlogPost>;

    internal sealed class CreateNewBlogPostCommandHandler : IRequestHandler<CreateNewBlogPostCommand, BlogPost>
    {
        public async Task<BlogPost> Handle(CreateNewBlogPostCommand request, CancellationToken cancellationToken)
        {
            var title = new Title(request.Title);
            var content = new Content(request.Content);
            var blogPost = BlogPost.New(request.BloggerId, title, content);
            //TODO - Persist.
            return blogPost;
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
