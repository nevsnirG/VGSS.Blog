using VGSS.Domain;
using VGSS.Domain.Ports;

namespace VGSS.MockPersistence;
internal sealed class GetUserImpl : IGetUser
{
    public Task<User> GetByUserId(UserId userId)
    {
        return Task.FromResult(
            SeedData.Users.SingleOrDefault(x => x.Key == userId) ?? User.New("Henk")
            );
    }

    public Task<IReadOnlyCollection<User>> GetUsersByIds(UserId[] ids)
    {
        return Task.FromResult(
            (IReadOnlyCollection<User>)SeedData.Users.Where(x => ids.Contains(x.Key)).ToList().AsReadOnly()
            );
    }
}
