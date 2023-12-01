using MediatR;
using VGSS.Domain.BlogAggregate;
using VGSS.Domain.Ports;
using VGSS.Domain.UserAggregate;

namespace VGSS.Application;
public static class BlogPostsOverview
{
    public sealed record class Query() : IRequest<IReadOnlyCollection<ViewModel>>;
    public sealed record class ViewModel(BlogPostId Id, string Title, uint Views, DateTimeOffset PostedAt, UserViewModel PostedBy);
    public sealed record class UserViewModel(string Username, UserId UserId);

    private static UserViewModel ToViewModel(this User user) =>
         new(user.Username, user.Id);

    private static ViewModel ToViewModelWithUser(this BlogPost blogPost, UserViewModel userViewModel) =>
        new(blogPost.Id, blogPost.Title, blogPost.Views, blogPost.PostedAt, userViewModel);

    internal sealed class ViewBlogPostsQueryHandler : IRequestHandler<Query, IReadOnlyCollection<ViewModel>>
    {
        private readonly IGetBlogPosts _getBlogPosts;
        private readonly IGetUser _getUser;

        public ViewBlogPostsQueryHandler(IGetBlogPosts getBlogPost, IGetUser getUser)
        {
            _getBlogPosts = getBlogPost;
            _getUser = getUser;
        }

        public async Task<IReadOnlyCollection<ViewModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var blogPosts = await _getBlogPosts.GetBlogPosts();

            var blogPostViewModels = new List<ViewModel>(blogPosts.Count);
            var userViewModels = await _getUser.GetUsersByIds(blogPosts.Select(x => x.PostedBy).ToArray());
            foreach (var blogPost in blogPosts)
            {
                var userViewModel = userViewModels.Single(u => blogPost.PostedBy == u.Id).ToViewModel();
                var blogPostViewModel = blogPost.ToViewModelWithUser(userViewModel);
                blogPostViewModels.Add(blogPostViewModel);
            }
            return blogPostViewModels;
        }
    }

}