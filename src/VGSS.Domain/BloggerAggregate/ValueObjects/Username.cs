using MinimalRichDomain;

namespace VGSS.Domain.BloggerAggregate.ValueObjects;
public sealed class Username : ValueObject
{
    public string Value { get; }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));
        if (value.Length < 3)
            throw new ArgumentOutOfRangeException(nameof(value), "Username can not be shorter than 3 characters.");
        if (value.Length > 128)
            throw new ArgumentOutOfRangeException(nameof(value), "Username can not be longer than 128 characters.");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Username username) =>
        username.Value;
}