using MediatR;
using VGSS.Application.ViewBlogPost;
using VGSS.Domain;

namespace VGSS.Web.Components.ViewBlogPosts;

public interface IViewBlogPosts
{
    Task<IReadOnlyCollection<BlogPost>> View();
}

internal sealed class ViewBlogPosts(IMediator mediator) : IViewBlogPosts
{
    private readonly IMediator _mediator = mediator;

    public async Task<IReadOnlyCollection<BlogPost>> View()
    {
        return await _mediator.Send(new ViewBlogPostsQuery());
    }
}
