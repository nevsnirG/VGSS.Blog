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
        FluentActions.Invoking(() => new Username(value!)).Should().ThrowExactly<ArgumentNullException>();
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    public void CanNotBeShorterThan3Characters(string value )
    {
        FluentActions.Invoking(() => new Username(value)).Should().ThrowExactly<ArgumentOutOfRangeException>().WithMessage("Username can not be shorter than 3 characters.*", "usernames can not be shorter than 3 characters");
    }

    [Fact]
    public void CanNotBeLongerThan128Characters()
    {
        var value = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";

        FluentActions.Invoking(() => new Username(value)).Should().ThrowExactly<ArgumentOutOfRangeException>().WithMessage("Username can not be longer than 128 characters.*", "usernames can not be longer than 128 characters");
    }
}
