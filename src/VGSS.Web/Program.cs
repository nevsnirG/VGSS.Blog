using AssembleMe.MicrosoftDependencyInjection;
using AutoWire.MicrosoftDependencyInjection;
using Microsoft.AspNetCore.Components;
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
    .AddMockPersistence()
    .AddMediatR(c =>
    {
        c.AutoRegisterRequestProcessors = true;
        c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
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