using MediatR;
using MinimalDomainEvents.Contract;
using VGSS.Domain.BloggerAggregate;

namespace VGSS.Domain.BlogPostAggregate.Events;
public sealed record class BlogPostViewed(BlogPostId BlogPostId, BloggerId ViewedBy, DateTimeOffset ViewedAt) : IDomainEvent, INotification;