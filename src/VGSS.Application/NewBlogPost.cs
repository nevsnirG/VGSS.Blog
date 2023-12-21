using MediatR;
using OneOf;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BlogPostAggregate.Events;
using VGSS.Domain.BlogPostAggregate.ValueObjects;
using VGSS.Domain.Ports;

namespace VGSS.Application;
public static class NewBlogPost
{
    //TODO - Return viewmodel.
    public sealed record class PostNewBlogPostCommand(BloggerId BloggerId, string Title, string Content) : IRequest<OneOf<BlogPost, ValidationFailed>>;

    internal sealed class PostNewBlogPostCommandHandler(IBloggerRepository bloggerRepository, IBlogPostRepository blogPostRepository) :
        IRequestHandler<PostNewBlogPostCommand, OneOf<BlogPost, ValidationFailed>>
    {
        public async Task<OneOf<BlogPost, ValidationFailed>> Handle(PostNewBlogPostCommand request, CancellationToken cancellationToken)
        {
            var user = await bloggerRepository.GetById(request.BloggerId)
                ?? throw new InvalidOperationException($"No user with id {request.BloggerId} exists.");

            var title = Title.Create(request.Title);
            if (!title.IsSuccess)
                return new ValidationFailed(title.Reason);

            var content = Content.Create(request.Content);
            if (!content.IsSuccess)
                return new ValidationFailed(content.Reason);

            var blogPost = user.PostNewBlogPost(title!, content!);

            await blogPostRepository.Save(blogPost);

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
