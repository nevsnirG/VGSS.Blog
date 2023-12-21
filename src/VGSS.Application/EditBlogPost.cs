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
            var newTitle = new Title(request.Title);
            var newContent = new Content(request.Content);

            blogPost.Edit(request.EditedBy, newTitle, newContent);
            await blogPostRepository.Save(blogPost);

            return blogPost;
        }
    }

    internal sealed class BlogPostEditedEventHandler : INotificationHandler<BlogPostEditedEvent>
    {
        public Task Handle(NewBlogPostPostedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"A new blog post titled '{notification.Title}' was created!");
            return Task.CompletedTask;
        }

        public Task Handle(BlogPostEditedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"The blog post titled '{notification.OldTitle}' was renamed to '{notification.NewTitle}'!");
            return Task.CompletedTask;
        }
    }
}
