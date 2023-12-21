using MinimalRichDomain;

namespace VGSS.Domain.BlogPostAggregate.ValueObjects;

public sealed class Title : ValueObject
{
    public string Value { get; }

    private Title(string value)
    {
        Value = value;
    }

    public static Result<Title?> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Fail<Title?>("Title can not be empty.");
        if (value.Length > 128)
            return Result.Fail<Title?>("Title can not be longer than 128 characters.");

        var title = new Title(value);
        return Result.Success(title);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Title title) =>
        title.Value;
}