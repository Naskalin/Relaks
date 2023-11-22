using System.Diagnostics;
using Microsoft.Extensions.FileProviders;
using Relaks;
using Relaks.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddRelaks();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.PostConfigure<StaticFileOptions>(o =>
{
    if (o.FileProvider is CompositeFileProvider compositeFileProvider)
    {
        var providers = compositeFileProvider.FileProviders.ToList();
        providers.Add(RelaksExtensions.FilesProvider());
        o.FileProvider = new CompositeFileProvider(providers);
    }
});

var app = builder.Build();
app.UseRelaks();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

var appOperation = app.Services.GetRequiredService<AppOperation>();
appOperation.OnRestart += RestartApp;

void RestartApp()
{
    app.StopAsync().ContinueWith(_ => app.Start());
}

app.Run();