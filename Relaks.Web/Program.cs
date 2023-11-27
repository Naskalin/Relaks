using Microsoft.Extensions.FileProviders;
using Relaks;
using Relaks.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRelaks();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();
app.UseRelaks();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new CompositeFileProvider(RelaksExtensions.FilesProvider(), app.Environment.WebRootFileProvider)
});
app.MapRazorComponents<Relaks.Index>()
    .DisableAntiforgery()
    .AddInteractiveServerRenderMode();

var appOperation = app.Services.GetRequiredService<AppOperation>();
appOperation.OnRestart += RestartApp;

void RestartApp()
{
    app.StopAsync().ContinueWith(_ => app.Start());
}

app.Run();