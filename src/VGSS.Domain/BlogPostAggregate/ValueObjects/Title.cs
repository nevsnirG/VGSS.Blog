using MinimalRichDomain;

namespace VGSS.Domain.BlogPostAggregate.ValueObjects;

public sealed class Title : ValueObject
{
    public string Value { get; }

    public Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value), "A title can not be empty.");
        if(value.Length > 128)
            throw new ArgumentOutOfRangeException(nameof(value), "Title can not be longer than 128 characters.");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Title title) =>
        title.Value;
}