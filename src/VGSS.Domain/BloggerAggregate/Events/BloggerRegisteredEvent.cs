using VGSS.Domain.BloggerAggregate.ValueObjects;

namespace VGSS.Domain.BloggerAggregate.Events;
public sealed record class BloggerRegisteredEvent(BloggerId BloggerId, Username Username, DateTimeOffset RegisteredAt, int Version) : IDomainEvent;