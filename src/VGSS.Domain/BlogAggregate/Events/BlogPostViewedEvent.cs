using VGSS.Domain.UserAggregate;
using MediatR;
using MinimalDomainEvents.Contract;

namespace VGSS.Domain.BlogAggregate.Events;
public sealed record class BlogPostViewed(BlogPostId BlogPostId, UserId ViewedBy, DateTimeOffset ViewedAt) : IDomainEvent, INotification;