using Microsoft.Extensions.FileProviders;
using Relaks;
using Relaks.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddRelaks();
builder.Services.AddServerSideBlazor();
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

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();

app.MapBlazorHub();
app.MapFallbackToPage("/Index");

app.Run();

var appOperation = new AppOperation();
appOperation.OnRestart += () =>
{
    Console.WriteLine(@">>> ok reload app");
};