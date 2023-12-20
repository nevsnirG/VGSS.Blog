using Microsoft.Extensions.DependencyInjection;
using VGSS.Domain.Ports;

namespace VGSS.MockPersistence;
public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddMockPersistence(this IServiceCollection services)
    {
        return services
            .AddScoped<IBloggerRepository, BloggerRepositoryMock>()
            .AddScoped<IBlogPostRepository, BlogPostRepositoryMock>()
            ;
    }
}
