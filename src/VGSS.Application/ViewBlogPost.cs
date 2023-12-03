using MediatR;
using VGSS.Domain.Ports;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BlogPostAggregate.Events;

namespace VGSS.Application;
public static class ViewBlogPost
{
    public sealed record class BloggerViewModel(string Username, BloggerId BloggerId);
    public sealed record class ViewModel(string Title, string Content, uint Views, BloggerViewModel PostedBy, DateTimeOffset PostedAt);
    private static BloggerViewModel ToViewModel(this Blogger user) =>
        new(user.Username, user.Id);
    private static ViewModel ToViewModelWithUser(this BlogPost blogPost, BloggerViewModel BloggerViewModel) =>
        new(blogPost.Title, blogPost.Content, blogPost.Views, BloggerViewModel, blogPost.PostedAt);

    public sealed record class Query(BlogPostId BlogPostId) : IRequest<ViewModel>;

    internal sealed class ViewBlogPostQueryHandler : IRequestHandler<Query, ViewModel>
    {
        private readonly IGetBlogPosts _getBlogPost;
        private readonly IGetBlogger _getUser;

        public ViewBlogPostQueryHandler(IGetBlogPosts getBlogPost, IGetBlogger getUser)
        {
            _getBlogPost = getBlogPost;
            _getUser = getUser;
        }

        public async Task<ViewModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var blogPost = await _getBlogPost.GetBlogPostById(request.BlogPostId);
            blogPost.View(BloggerId.Empty);

            var user = await _getUser.GetByBloggerId(blogPost.PostedBy);
            var blogPostViewModel = blogPost.ToViewModelWithUser(user.ToViewModel());
            return blogPostViewModel;
        }
    }

    internal sealed class BlogPostViewedEventHandler : INotificationHandler<BlogPostViewed>
    {
        public Task Handle(BlogPostViewed notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"User '{notification.ViewedBy.Value}' viewed your blog post!");
            return Task.CompletedTask;
        }
    }
}
