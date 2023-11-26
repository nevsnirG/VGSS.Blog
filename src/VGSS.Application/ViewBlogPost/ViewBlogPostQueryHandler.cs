using MediatR;
using VGSS.Domain;
using VGSS.Domain.Ports;

namespace VGSS.Application.ViewBlogPost;
internal sealed class ViewBlogPostQueryHandler : IRequestHandler<ViewBlogPostQuery, BlogPost>
{
    private readonly IGetBlogPosts _getBlogPost;

    public ViewBlogPostQueryHandler(IGetBlogPosts getBlogPost)
    {
        _getBlogPost = getBlogPost;
    }

    public async Task<BlogPost> Handle(ViewBlogPostQuery request, CancellationToken cancellationToken)
    {
        var blogPost = await _getBlogPost.GetBlogPostById(request.BlogPostId);
        blogPost.View(request.UserId);
        return blogPost;
    }
}
