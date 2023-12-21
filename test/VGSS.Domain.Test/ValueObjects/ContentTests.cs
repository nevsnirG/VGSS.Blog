using VGSS.Domain.BlogPostAggregate.ValueObjects;

namespace VGSS.Domain.Test.ValueObjects;
public class ContentTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void CanNotBeNullOrWhiteSpace(string? value)
    {
        var result = Content.Create(value);

        result.IsSuccess.Should().BeFalse();
        result.Reason.Should().Be("Content can not be empty.");
    }
}
