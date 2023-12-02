namespace VGSS.Domain.BloggerAggregate.ValueObjects;

public sealed class Title : ValueObject
{
    public string Value { get; }

    public Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("A title can not be empty.");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Title title) =>
        title.Value;
}