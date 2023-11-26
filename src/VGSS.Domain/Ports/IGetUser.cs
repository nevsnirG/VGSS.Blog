namespace VGSS.Domain.Ports;
public interface IGetUser
{
    public Task<User> GetByUserId(UserId userId);
}
