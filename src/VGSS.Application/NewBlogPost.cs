using MediatR;
using VGSS.Domain;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BlogPostAggregate.Events;
using VGSS.Domain.BlogPostAggregate.ValueObjects;
using VGSS.Domain.Ports;

namespace VGSS.Application;
public static class NewBlogPost
{
    public sealed record class PostNewBlogPostCommand(BloggerId BloggerId, string Title, string Content) : IRequest<BlogPost>;

    internal sealed class PostNewBlogPostCommandHandler(IBloggerRepository bloggerRepository, IBlogPostRepository blogPostRepository) : IRequestHandler<PostNewBlogPostCommand, BlogPost>
    {
        public async Task<BlogPost> Handle(PostNewBlogPostCommand request, CancellationToken cancellationToken)
        {
            var user = await bloggerRepository.GetById(request.BloggerId);

            var title = Title.Create(request.Title);
            if (!title.IsSuccess) { throw new NotImplementedException(); }

            var content = Content.Create(request.Content);
            if (!content.IsSuccess) { throw new NotImplementedException(); }

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
