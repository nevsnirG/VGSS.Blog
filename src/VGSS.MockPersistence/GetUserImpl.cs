using VGSS.Domain.Ports;
using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BloggerAggregate.ValueObjects;

namespace VGSS.MockPersistence;
internal sealed class GetUserImpl : IGetBlogger
{
    public Task<Blogger> GetByBloggerId(BloggerId BloggerId)
    {
        return Task.FromResult(
            (Blogger)(SeedData.Bloggers.SingleOrDefault(x => x.Id == BloggerId) ?? Blogger.New(new Username("Henk")))
            );
    }

    public Task<IReadOnlyCollection<Blogger>> GetUsersByIds(BloggerId[] ids)
    {
        return Task.FromResult(
            (IReadOnlyCollection<Blogger>)SeedData.Bloggers.Where(x => ids.Contains(x.Id)).ToList().AsReadOnly()
            );
    }
}
