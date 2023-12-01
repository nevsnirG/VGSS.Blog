namespace VGSS.Domain.UserAggregate;
public readonly record struct Username(string Value)
{
    public static implicit operator string(Username username) => username.Value;
}