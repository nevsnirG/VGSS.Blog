using MediatR;
using MinimalDomainEvents.Contract;

namespace VGSS.Domain.Entities.BlogPost;
public sealed record class BlogPostViewed(BlogPostId BlogPostId, UserId ViewedBy, DateTimeOffset ViewedAt) : IDomainEvent, INotification;