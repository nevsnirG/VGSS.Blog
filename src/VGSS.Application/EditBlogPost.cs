using MediatR;
using OneOf;
using OneOf.Types;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BlogPostAggregate.Events;
using VGSS.Domain.BlogPostAggregate.ValueObjects;
using VGSS.Domain.Ports;

namespace VGSS.Application;
public static class EditBlogPost
{
    public sealed record class EditBlogPostCommand(BlogPostId BlogPostId, BloggerId EditedBy, string Title, string Content) : IRequest<OneOf<BlogPost, NotFound, ValidationFailed>>;

    internal sealed class EditBlogPostCommandHandler(IBlogPostRepository blogPostRepository) : IRequestHandler<EditBlogPostCommand, OneOf<BlogPost, NotFound, ValidationFailed>>
    {
        public async Task<OneOf<BlogPost, NotFound, ValidationFailed>> Handle(EditBlogPostCommand request, CancellationToken cancellationToken)
        {
            var blogPost = await blogPostRepository.GetById(request.BlogPostId);
            if (blogPost is null)
                return new NotFound();

            var newTitle = Title.Create(request.Title);
            if (!newTitle.IsSuccess)
                return new ValidationFailed(newTitle.Reason);

            var newContent = Content.Create(request.Content);
            if(!newContent.IsSuccess)
                return new ValidationFailed(newContent.Reason);

            blogPost.Edit(request.EditedBy, newTitle!, newContent!);

            await blogPostRepository.Save(blogPost);

            return blogPost;
        }
    }

    internal sealed class BlogPostEditedEventHandler : INotificationHandler<BlogPostEditedEvent>
    {
        public Task Handle(BlogPostEditedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"The blog post titled '{notification.OldTitle}' was renamed to '{notification.NewTitle}'!");
            return Task.CompletedTask;
        }
    }
}
