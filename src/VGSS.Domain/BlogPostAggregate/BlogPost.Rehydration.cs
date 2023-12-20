using MinimalRichDomain;
using VGSS.Domain.BlogPostAggregate.Events;

namespace VGSS.Domain.BloggerAggregate;
public partial class BlogPost : IApplyEvent<NewBlogPostPostedEvent>, IApplyEvent<BlogPostViewedEvent>, IApplyEvent<BlogPostEditedEvent>
{
    protected override void ValidateState()
    {
        if (PostedBy == BloggerId.Empty)
            throw new InvalidOperationException("BlogPost is in a corrupt state. BlogPostId is empty.");
        if (PostedAt == DateTimeOffset.MinValue)
            throw new InvalidOperationException("BlogPost is in a corrupt state. PostedAt is missing.");
        if (Title is null)
            throw new InvalidOperationException("BlogPost is in a corrupt state. Title is missing.");
        if (Content is null)
            throw new InvalidOperationException("BlogPost is in a corrupt state. PostedAt is missing.");
    }

    void IApplyEvent<NewBlogPostPostedEvent>.Apply(NewBlogPostPostedEvent @event)
    {
        PostedBy = @event.PostedBy;
        PostedAt = @event.PostedAt;
        Title = @event.Title;
        Content = @event.Content;
        Views = 0;
    }

    void IApplyEvent<BlogPostViewedEvent>.Apply(BlogPostViewedEvent @event)
    {
        Views++;
    }

    void IApplyEvent<BlogPostEditedEvent>.Apply(BlogPostEditedEvent @event)
    {
        Title = @event.NewTitle;
        Content = @event.NewContent;
    }
}
