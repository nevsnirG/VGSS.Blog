using VGSS.Domain.BloggerAggregate;


namespace VGSS.Domain.Ports;
public interface IBloggerRepository
{
    Task<Blogger> GetById(BloggerId bloggerId);
    Task<IReadOnlyCollection<Blogger>> GetByIds(BloggerId[] bloggerIds);
}
