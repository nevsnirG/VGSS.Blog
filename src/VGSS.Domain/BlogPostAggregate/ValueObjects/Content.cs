using MinimalRichDomain;

namespace VGSS.Domain.BlogPostAggregate.ValueObjects;

public sealed class Content : ValueObject
{
    public string Value { get; }

    public Content(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Content can not be empty.");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Content content) =>
        content.Value;
}
