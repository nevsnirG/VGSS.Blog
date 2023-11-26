using VGSS.Domain;
using VGSS.Domain.Ports;

namespace VGSS.MockPersistence;
internal sealed class GetUserImpl : IGetUser
{
    private readonly IDictionary<UserId, User> _users;

    public GetUserImpl()
    {
        _users = new Dictionary<UserId, User>();
    }

    public Task<User> GetByUserId(UserId userId)
    {
        return Task.FromResult(
            _users[userId]
            );
    }
}
