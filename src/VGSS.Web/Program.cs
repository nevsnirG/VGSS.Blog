using AssembleMe;
using AutoWire.MicrosoftDependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.Net.Http.Headers;
using System.Reflection;
using VGSS.Application;
using VGSS.MockPersistence;
using VGSS.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddAssembler(b => b.AddMicrosoftDependencyInjectionWiring())
    .AddApplication()
    .AddMockPersistence();

builder.Services.Configure<StaticFileOptions>(options =>
{
    options.OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 24 * 7;
        ctx.Context.Response.Headers[HeaderNames.CacheControl] =
            "public,max-age=" + durationInSeconds;
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddAdditionalAssemblies(GetLoadedRazorComponentAssemblies())
   .AddInteractiveServerRenderMode();

app.Run();

static Assembly[] GetLoadedRazorComponentAssemblies()
{
    var executingAssembly = Assembly.GetExecutingAssembly();
    return AppDomain.CurrentDomain.GetAssemblies()
        .Where(assembly => 
            assembly != executingAssembly && assembly.GetTypes().Any(type => typeof(ComponentBase).IsAssignableFrom(type)))
        .ToArray();
}