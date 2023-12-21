using MediatR;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BlogPostAggregate.Events;
using VGSS.Domain.BlogPostAggregate.ValueObjects;
using VGSS.Domain.Ports;

namespace VGSS.Application;
public static class EditBlogPost
{
    public sealed record class EditBlogPostCommand(BlogPostId BlogPostId, BloggerId EditedBy, string Title, string Content) : IRequest<BlogPost>;

    internal sealed class EditBlogPostCommandHandler(IBlogPostRepository blogPostRepository) : IRequestHandler<EditBlogPostCommand, BlogPost>
    {
        public async Task<BlogPost> Handle(EditBlogPostCommand request, CancellationToken cancellationToken)
        {
            var blogPost = await blogPostRepository.GetById(request.BlogPostId);

            var newTitle = Title.Create(request.Title);
            if (!newTitle.IsSuccess) { throw new NotImplementedException(); }

            var newContent = Content.Create(request.Content);
            if(!newContent.IsSuccess) { throw new NotImplementedException(); }

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
