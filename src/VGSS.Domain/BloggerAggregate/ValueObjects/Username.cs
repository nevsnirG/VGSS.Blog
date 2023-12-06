using MinimalRichDomain;

namespace VGSS.Domain.BloggerAggregate.ValueObjects;
public sealed class Username : ValueObject
{
    public string Value { get; }

    public Username(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Username username) =>
        username.Value;
}