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
        FluentActions.Invoking(() => new Content(value!)).Should().ThrowExactly<ArgumentNullException>();
    }
}
