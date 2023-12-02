using VGSS.Domain.BloggerAggregate.Events;

namespace VGSS.Domain.BloggerAggregate;
public partial class BlogPost : IApplyEvent<NewBlogPostPostedEvent>
{
    protected override void ValidateRehydration()
    {
        if(PostedBy == BloggerId.Empty)
            throw new InvalidOperationException("Blogger rehydrated in corrupt state. BlogPostId is empty.");
        if(PostedAt == DateTimeOffset.MinValue)
            throw new InvalidOperationException("Blogger rehydrated in corrupt state. PostedAt is missing.");
        if (Title is null)
            throw new InvalidOperationException("Blogger rehydrated in corrupt state. Title is missing.");
        if (Content is null)
            throw new InvalidOperationException("Blogger rehydrated in corrupt state. PostedAt is missing.");
    }

    void IApplyEvent<NewBlogPostPostedEvent>.Apply(NewBlogPostPostedEvent @event)
    {
        PostedBy = @event.PostedBy;
        PostedAt = @event.PostedAt;
        Title = @event.Title;
        Content = @event.Content;
        Views = 0;
    }
}
