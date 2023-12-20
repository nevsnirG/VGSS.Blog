using VGSS.Domain.BloggerAggregate;

namespace VGSS.Domain.BlogPostAggregate.Events;
public sealed record class BlogPostViewedEvent(BlogPostId Id, BloggerId ViewedBy, DateTimeOffset ViewedAt, int Version) : IDomainEvent;