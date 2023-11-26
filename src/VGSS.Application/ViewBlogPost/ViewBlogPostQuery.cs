using MediatR;
using VGSS.Domain;

namespace VGSS.Application.ViewBlogPost;
public sealed record class ViewBlogPostQuery(UserId UserId, BlogPostId BlogPostId) : IRequest<BlogPost>;