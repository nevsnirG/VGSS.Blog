using AutoWire.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace VGSS.Web.ViewBlogPosts;
internal static class DependencyInjectionConfiguration
{
    [AutoWire]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "AutoWire")]
    private static IServiceCollection AddViewBlogPosts(this IServiceCollection services)
    {
        return services
            .AddScoped<IViewBlogPosts, ViewBlogPosts>()
            ;
    }
}
