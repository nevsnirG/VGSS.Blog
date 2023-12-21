using VGSS.Domain.BlogPostAggregate.ValueObjects;

namespace VGSS.Domain.Test.ValueObjects;
public class TitleTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void CanNotBeNullOrWhiteSpace(string? value)
    {
        var result = Title.Create(value);

        result.IsSuccess.Should().BeFalse();
        result.Reason.Should().Be("Title can not be empty.");
    }

    [Fact]
    public void CanNotBeLongerThan128Characters()
    {
        var value = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";

        var result = Title.Create(value);

        result.IsSuccess.Should().BeFalse();
        result.Reason.Should().Be("Title can not be longer than 128 characters.");
    }
}
