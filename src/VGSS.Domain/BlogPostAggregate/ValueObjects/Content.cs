using MinimalRichDomain;

namespace VGSS.Domain.BlogPostAggregate.ValueObjects;

public sealed class Content : ValueObject
{
    public string Value { get; }

    private Content(string value)
    {
        Value = value;
    }

    public static Result<Content?> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Fail<Content?>("Content can not be empty.");

        var content = new Content(value);
        return Result.Success(content);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Content content) =>
        content.Value;
}
