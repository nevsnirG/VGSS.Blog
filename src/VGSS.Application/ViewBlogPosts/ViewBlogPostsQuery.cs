using MediatR;
using System.Collections.Generic;
using VGSS.Domain;

namespace VGSS.Application.ViewBlogPost;
public sealed record class ViewBlogPostsQuery() : IRequest<IReadOnlyCollection<BlogPost>>;