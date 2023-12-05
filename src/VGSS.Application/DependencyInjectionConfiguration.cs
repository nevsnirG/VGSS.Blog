using Microsoft.Extensions.DependencyInjection;
using MinimalDomainEvents.Dispatcher;
using MinimalDomainEvents.Dispatcher.MediatR;

namespace VGSS.Application;
public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            })
            .AddDomainEventDispatcher(b => 
                b.AddMediatorDispatcher()
            )
            ;
    }
}
