using MediatR;
using VGSS.Domain;
using VGSS.Domain.Ports;

namespace VGSS.Application.NewBlogPost;
internal sealed class CreateNewBlogPostCommandHandler : IRequestHandler<CreateNewBlogPostCommand, BlogPost>
{
    private readonly IGetUser _getUser;

    public CreateNewBlogPostCommandHandler(IGetUser getUser)
    {
        _getUser = getUser;
    }

    public async Task<BlogPost> Handle(CreateNewBlogPostCommand request, CancellationToken cancellationToken)
    {
        var user = await _getUser.GetByUserId(request.UserId);
        return user.PostNewBlogPost(request.Title, request.Content);
    }
}
