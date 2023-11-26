﻿using MediatR;
using MinimalDomainEvents.Contract;

namespace VGSS.Domain;
public class NewBlogPostPostedEvent : IDomainEvent, INotification
{
    public UserId PostedBy { get; }
    public string Title { get; }
    public string Content { get; }
    public DateTimeOffset PostedAt { get; }

    public NewBlogPostPostedEvent(UserId postedBy, string title, string content, DateTimeOffset postedAt)
    {
        PostedBy = postedBy;
        Title = title;
        Content = content;
        PostedAt = postedAt;
    }
}
