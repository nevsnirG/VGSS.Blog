using MinimalRichDomain;

namespace VGSS.Domain.BloggerAggregate.ValueObjects;
public sealed class Username : ValueObject
{
    public string Value { get; }

    private Username(string value)
    {
        Value = value;
    }

    public static Result<Username?> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Fail<Username?>("Username can not be empty.");
        if (value.Length < 3)
            return Result.Fail<Username?>("Username can not be shorter than 3 characters.");
        if (value.Length > 128)
            return Result.Fail<Username?>("Username can not be longer than 128 characters.");

        var username = new Username(value);
        return Result.Success(username);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Username username) =>
        username.Value;
}