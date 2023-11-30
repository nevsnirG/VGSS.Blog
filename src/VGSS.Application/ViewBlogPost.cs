using MediatR;
using VGSS.Domain.BlogAggregate;
using VGSS.Domain.BlogAggregate.Events;
using VGSS.Domain.Ports;
using VGSS.Domain.UserAggregate;

namespace VGSS.Application;
public static class ViewBlogPost
{
    public sealed record class UserViewModel(string Username, UserId UserId);
    public sealed record class ViewModel(string Title, string Content, uint Views, UserViewModel PostedBy, DateTimeOffset PostedAt);
    private static UserViewModel ToViewModel(this User user) =>
        new(user.Username, user.Id);
    private static ViewModel ToViewModelWithUser(this BlogPost blogPost, UserViewModel userViewModel) =>
        new(blogPost.Title, blogPost.Content, blogPost.Views, userViewModel, blogPost.PostedAt);

    public sealed record class Query(BlogPostId BlogPostId) : IRequest<ViewModel>;

    internal sealed class ViewBlogPostQueryHandler : IRequestHandler<Query, ViewModel>
    {
        private readonly IGetBlogPosts _getBlogPost;
        private readonly IGetUser _getUser;

        public ViewBlogPostQueryHandler(IGetBlogPosts getBlogPost, IGetUser getUser)
        {
            _getBlogPost = getBlogPost;
            _getUser = getUser;
        }

        public async Task<ViewModel> Handle(Query request, CancellationToken cancellationToken)
        {
            var blogPost = await _getBlogPost.GetBlogPostById(request.BlogPostId);
            blogPost.View();

            var user = await _getUser.GetByUserId(blogPost.PostedBy);
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
