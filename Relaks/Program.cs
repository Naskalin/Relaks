// using Blazorise;
// using Blazorise.Bootstrap5;
// using Blazorise.Icons.FontAwesome;

using BlazorBootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Photino.Blazor;
using PhotinoNET;
using Relaks;
using Relaks.Database;
using Relaks.Managers;

if (OperatingSystem.IsWindows())
{
    Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
    Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
}

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddBlazorDesktop();
var projectDir = AppDomain.CurrentDomain.BaseDirectory;
var relaksConfig = RelaksConfigManager.GetOrCreateConfig(projectDir);
builder.Services.AddSingleton(relaksConfig);

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlite(relaksConfig.SqliteConnectionString())
);

builder.Services.RegisterManagers();
// builder.Services.RegisterCultures();

builder.Services.AddBlazorBootstrap();
// builder.Services.AddBlazorise(o => { o.Immediate = true; })
//     .AddBootstrap5Providers()
//     .AddFontAwesomeIcons();

var app = builder.Build();


// var env = app.Services.GetRequiredService<IHostEnvironment>();
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;

db.Database.Migrate();
if (isDevelopment)
{
    // new Relaks.Database.Seeders.DatabaseSeeder(db).SeedAll();
}

// EntryEvents.CheckAndRefresh(db);
// EntryInfoEvents.CheckAndRefresh(db);

var rootComponents = app.Services.GetRequiredService<BlazorWindowRootComponents>();
rootComponents.Add(typeof(App), "#app");
var mainWindow = app.Services.GetRequiredService<PhotinoWindow>();
mainWindow
    .SetWidth(1024)
    .SetHeight(768)
    .SetIconFile("favicon.ico")
    .SetTitle("Relaks beta")
    .SetDevToolsEnabled(isDevelopment)
    ;
mainWindow.Centered = true;

var windowManager = app.Services.GetRequiredService<PhotinoWebViewManager>();
mainWindow.RegisterCustomSchemeHandler(PhotinoWebViewManager.BlazorAppScheme, windowManager.HandleWebRequest);

AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
{
    mainWindow.OpenAlertWindow("Fatal exception", error.ExceptionObject.ToString());
};

await app.StartAsync();
windowManager.Navigate("/");
mainWindow.WaitForClose();
await app.StopAsync();
await app.WaitForShutdownAsync();