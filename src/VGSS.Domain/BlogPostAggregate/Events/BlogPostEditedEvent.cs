using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BlogPostAggregate.ValueObjects;

namespace VGSS.Domain.BlogPostAggregate.Events;
public sealed record class BlogPostEditedEvent(BlogPostId Id, BloggerId EditedBy, DateTimeOffset EditedAt, Title OldTitle, Title NewTitle, Content NewContent, int Version) : IDomainEvent;