using MediatR;
using VGSS.Domain.Ports;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BlogPostAggregate.Events;
using OneOf.Types;
using OneOf;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VGSS.Application;
public static class ViewBlogPost
{
    public sealed record class BloggerViewModel(string Username, BloggerId BloggerId);
    public sealed record class ViewModel(string Title, string Content, uint Views, BloggerViewModel PostedBy, DateTimeOffset PostedAt);
    private static BloggerViewModel ToViewModel(this Blogger user) =>
        new(user.Username, user.Id);
    private static ViewModel ToViewModelWithUser(this BlogPost blogPost, BloggerViewModel BloggerViewModel) =>
        new(blogPost.Title, blogPost.Content, blogPost.Views, BloggerViewModel, blogPost.PostedAt);

    public sealed record class Query(BlogPostId BlogPostId) : IRequest<OneOf<ViewModel, NotFound>>;

    internal sealed class ViewBlogPostQueryHandler(IBlogPostRepository getBlogPost, IBloggerRepository getUser) : IRequestHandler<Query, OneOf<ViewModel, NotFound>>
    {
        public async Task<OneOf<ViewModel, NotFound>> Handle(Query request, CancellationToken cancellationToken)
        {
            var blogPost = await getBlogPost.GetById(request.BlogPostId);

            if (blogPost is null)
                return new NotFound();

            var user = await getUser.GetById(blogPost.PostedBy)
                ?? throw new InvalidOperationException($"No user with id {blogPost.PostedBy} exists.");

            blogPost.View(BloggerId.Empty);

            var blogPostViewModel = blogPost.ToViewModelWithUser(user.ToViewModel());
            return blogPostViewModel;
        }
    }

    internal sealed class BlogPostViewedEventHandler : INotificationHandler<BlogPostViewedEvent>
    {
        public Task Handle(BlogPostViewedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"User '{notification.ViewedBy.Value}' viewed your blog post!");
            return Task.CompletedTask;
        }
    }
}
