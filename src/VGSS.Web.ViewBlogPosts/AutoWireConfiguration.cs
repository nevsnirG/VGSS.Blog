using AutoWire.Contract;
using Microsoft.Extensions.DependencyInjection;

[assembly: AutoWire]

namespace VGSS.Web.ViewBlogPosts;
internal static class AutoWireConfiguration
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
