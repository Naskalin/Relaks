using Relaks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddRelaks();
builder.Services.AddServerSideBlazor();

var app = builder.Build();
app.UseRelaks();

// app.UseHttpsRedirection();
app.UseStaticFiles(
    new StaticFileOptions()
    {
        FileProvider = RelaksExtensions.FilesProvider(),
    }
);

app.MapRazorPages();

app.MapBlazorHub();
app.MapFallbackToPage("/Index");

app.Run();