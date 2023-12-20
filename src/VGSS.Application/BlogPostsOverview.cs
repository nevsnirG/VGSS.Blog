using MediatR;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.Ports;

namespace VGSS.Application;
public static class BlogPostsOverview
{
    public sealed record class Query() : IRequest<IReadOnlyCollection<ViewModel>>;
    public sealed record class ViewModel(BlogPostId Id, string Title, uint Views, DateTimeOffset PostedAt, BloggerViewModel PostedBy);
    public sealed record class BloggerViewModel(string Username, BloggerId BloggerId);

    private static BloggerViewModel ToViewModel(this Blogger user) =>
         new(user.Username, user.Id);

    private static ViewModel ToViewModelWithBlogger(this BlogPost blogPost, BloggerViewModel BloggerViewModel) =>
        new(blogPost.Id, blogPost.Title, blogPost.Views, blogPost.PostedAt, BloggerViewModel);

    internal sealed class ViewBlogPostsQueryHandler(IBlogPostRepository blogPostRepository, IBloggerRepository bloggerRepository) : IRequestHandler<Query, IReadOnlyCollection<ViewModel>>
    {
        public async Task<IReadOnlyCollection<ViewModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var blogPosts = await blogPostRepository.GetAll();

            var blogPostViewModels = new List<ViewModel>(blogPosts.Count);
            var BloggerViewModels = await bloggerRepository.GetByIds(blogPosts.Select(x => x.PostedBy).ToArray());
            foreach (var blogPost in blogPosts)
            {
                var BloggerViewModel = BloggerViewModels.Single(u => blogPost.PostedBy == u.Id).ToViewModel();
                var blogPostViewModel = blogPost.ToViewModelWithBlogger(BloggerViewModel);
                blogPostViewModels.Add(blogPostViewModel);
            }
            return blogPostViewModels;
        }
    }

}