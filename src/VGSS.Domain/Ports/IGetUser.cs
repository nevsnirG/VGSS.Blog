namespace VGSS.Domain.Ports;
public interface IGetUser
{
    Task<User> GetByUserId(UserId userId);
    Task<IReadOnlyCollection<User>> GetUsersByIds(UserId[] ids);
}
