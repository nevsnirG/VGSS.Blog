using VGSS.Domain.BloggerAggregate.ValueObjects;

namespace VGSS.Domain.Test.ValueObjects;
public class UsernameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void CanNotBeNullOrWhiteSpace(string? value)
    {
        var result = Username.Create(value);

        result.IsSuccess.Should().BeFalse();
        result.Reason.Should().Be("Username can not be empty.");
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    public void CanNotBeShorterThan3Characters(string value)
    {
        var result = Username.Create(value);

        result.IsSuccess.Should().BeFalse();
        result.Reason.Should().Be("Username can not be shorter than 3 characters.");
    }

    [Fact]
    public void CanNotBeLongerThan128Characters()
    {
        var value = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";

        var result = Username.Create(value);

        result.IsSuccess.Should().BeFalse();
        result.Reason.Should().Be("Username can not be longer than 128 characters.");
    }
}
