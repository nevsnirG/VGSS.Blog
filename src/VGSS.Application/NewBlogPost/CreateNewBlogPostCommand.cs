using MediatR;
using VGSS.Domain;

namespace VGSS.Application.NewBlogPost;
public sealed record class CreateNewBlogPostCommand(UserId UserId, string Title, string Content) : IRequest<BlogPost>;