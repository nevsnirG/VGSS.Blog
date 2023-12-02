﻿using MediatR;
using MinimalDomainEvents.Contract;
using VGSS.Domain.BloggerAggregate.ValueObjects;

namespace VGSS.Domain.BloggerAggregate.Events;
public record NewBlogPostPostedEvent(BlogPostId Id, BloggerId PostedBy, Title Title, Content Content, DateTimeOffset PostedAt) : IDomainEvent, INotification;
