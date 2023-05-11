using Relaks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddRelaks();
builder.Services.AddServerSideBlazor();

var app = builder.Build();
app.UseRelaks();

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();