using MediatR;
using VGSS.Domain;
using VGSS.Domain.Ports;

namespace VGSS.Application.ViewBlogPost;
internal sealed class ViewBlogPostsQueryHandler : IRequestHandler<ViewBlogPostsQuery, IReadOnlyCollection<BlogPost>>
{
    private readonly IGetBlogPosts _getBlogPosts;

    public ViewBlogPostsQueryHandler(IGetBlogPosts getBlogPost)
    {
        _getBlogPosts = getBlogPost;
    }

    public async Task<IReadOnlyCollection<BlogPost>> Handle(ViewBlogPostsQuery request, CancellationToken cancellationToken)
    {
        return await _getBlogPosts.GetBlogPosts();
    }
}
