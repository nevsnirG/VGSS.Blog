using VGSS.Domain.BloggerAggregate;


namespace VGSS.Domain.Ports;
public interface IGetBlogger
{
    Task<Blogger> GetByBloggerId(BloggerId BloggerId);
    Task<IReadOnlyCollection<Blogger>> GetUsersByIds(BloggerId[] ids);
}
