using VGSS.Web.Components;
using VGSS.Application;
using VGSS.MockPersistence;
using VGSS.Web.Components.ViewBlogPosts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddScoped<IViewBlogPosts, ViewBlogPosts>()
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
    .AddInteractiveServerRenderMode();

app.Run();
