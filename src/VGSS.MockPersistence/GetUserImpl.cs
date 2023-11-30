using VGSS.Domain.Ports;
using VGSS.Domain.UserAggregate;

namespace VGSS.MockPersistence;
internal sealed class GetUserImpl : IGetUser
{
    public Task<User> GetByUserId(UserId userId)
    {
        return Task.FromResult(
            SeedData.Users.SingleOrDefault(x => x.Id == userId) ?? User.New(new Username("Henk"))
            );
    }

    public Task<IReadOnlyCollection<User>> GetUsersByIds(UserId[] ids)
    {
        return Task.FromResult(
            (IReadOnlyCollection<User>)SeedData.Users.Where(x => ids.Contains(x.Id)).ToList().AsReadOnly()
            );
    }
}
