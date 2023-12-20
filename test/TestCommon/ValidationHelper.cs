namespace VGSS.TestCommon;
public class ValidationHelper
{
    public static TAggregate ValidateRehydration<TAggregate>(object id, object domainEvents) where TAggregate : class
    {
        return (Activator.CreateInstance(typeof(TAggregate), id, domainEvents) as TAggregate)!;
    }
}
