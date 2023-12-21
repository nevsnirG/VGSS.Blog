using FluentAssertions;
using FluentAssertions.Equivalency;

namespace VGSS.TestCommon;
public class ValidationHelper
{
    public static TAggregate ValidateRehydration<TAggregate>(object id, object domainEvents) where TAggregate : class
    {
        return (Activator.CreateInstance(typeof(TAggregate), id, domainEvents) as TAggregate)!;
    }

    public static Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> DefaultEquivalencyAssertionOptions<T>()
    {
        return (options) =>
            options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTimeOffset>();
    }
}
