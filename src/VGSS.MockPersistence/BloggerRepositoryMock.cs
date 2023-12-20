using VGSS.Domain.BloggerAggregate;
using VGSS.Domain.BloggerAggregate.ValueObjects;
using VGSS.Domain.Ports;

namespace VGSS.MockPersistence;
internal sealed class BloggerRepositoryMock : IBloggerRepository
{
    public Task<Blogger> GetById(BloggerId BloggerId)
    {
        return Task.FromResult(
            (Blogger)(SeedData.Bloggers.SingleOrDefault(x => x.Id == BloggerId) ?? Blogger.New(new Username("Henk")))
            );
    }

    public Task<IReadOnlyCollection<Blogger>> GetByIds(BloggerId[] bloggerIds)
    {
        return Task.FromResult(
            (IReadOnlyCollection<Blogger>)SeedData.Bloggers.Where(x => bloggerIds.Contains(x.Id)).ToList().AsReadOnly()
            );
    }
}
