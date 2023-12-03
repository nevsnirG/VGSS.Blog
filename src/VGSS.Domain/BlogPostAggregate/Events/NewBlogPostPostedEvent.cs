using MediatR;
using MinimalDomainEvents.Contract;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BlogPostAggregate.ValueObjects;

namespace VGSS.Domain.BlogPostAggregate.Events;
public record NewBlogPostPostedEvent(BlogPostId Id, BloggerId PostedBy, Title Title, Content Content, DateTimeOffset PostedAt) : IDomainEvent, INotification;
