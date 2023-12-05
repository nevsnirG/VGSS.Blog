using VGSS.Domain.BloggerAggregate;

namespace VGSS.Domain.BlogPostAggregate.Events;
public sealed record class BlogPostViewedEvent(BlogPostId BlogPostId, BloggerId ViewedBy, DateTimeOffset ViewedAt, int Version) : IDomainEvent;