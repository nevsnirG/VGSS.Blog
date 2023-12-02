using MediatR;
using MinimalDomainEvents.Contract;

namespace VGSS.Domain.BloggerAggregate.Events;
public sealed record class BlogPostViewed(BlogPostId BlogPostId, BloggerId ViewedBy, DateTimeOffset ViewedAt) : IDomainEvent, INotification;