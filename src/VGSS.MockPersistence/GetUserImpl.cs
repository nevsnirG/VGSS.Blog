using VGSS.Domain;
using VGSS.Domain.Ports;

namespace VGSS.MockPersistence;
internal sealed class GetUserImpl : IGetUser
{
    private readonly List<User> _users;

    public GetUserImpl()
    {
        _users = new List<User>();
    }

    public Task<User> GetByUserId(UserId userId)
    {
        return Task.FromResult(
            _users.SingleOrDefault(x => x.Key == userId) ?? User.New("Henk")
            );
    }

    internal void AddUser(User user)
    {
        _users.Add(user);
    }
}
