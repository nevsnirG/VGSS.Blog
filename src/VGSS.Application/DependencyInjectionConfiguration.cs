using Microsoft.Extensions.DependencyInjection;
using MinimalDomainEvents.Dispatcher.MediatR;

namespace VGSS.Application;
public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(c =>
            {
                c.AutoRegisterRequestProcessors = true;
                c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            })
            .AddMediatorDispatcher()
            ;
    }
}
