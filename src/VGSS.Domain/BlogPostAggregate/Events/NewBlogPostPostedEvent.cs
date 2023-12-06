using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BlogPostAggregate.ValueObjects;

namespace VGSS.Domain.BlogPostAggregate.Events;
public sealed record class NewBlogPostPostedEvent(BlogPostId Id, BloggerId PostedBy, Title Title, Content Content, DateTimeOffset PostedAt, int Version) : IDomainEvent;