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
        FluentActions.Invoking(() => new Title(value!)).Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void CanNotBeLongerThan128Characters()
    {
        var value = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";

        FluentActions.Invoking(() => new Title(value)).Should().ThrowExactly<ArgumentOutOfRangeException>().WithMessage("Title can not be longer than 128 characters.*", "usernames can not be longer than 128 characters");
    }
}
