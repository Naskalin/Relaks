using Relaks;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args = args,
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRelaks();
builder.Services.AddAuthorization();
builder.Services.AddServerSideBlazor();

var app = builder.Build();
app.UseRelaks();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }
// app.UseHttpsRedirection();
//
app.UseHttpsRedirection();
app.UseStaticFiles();

// app.UseRouting();

// app.UseAuthorization();

app.MapRazorPages();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();