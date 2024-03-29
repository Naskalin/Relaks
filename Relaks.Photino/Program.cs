using System.Diagnostics;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Photino.Blazor;
using PhotinoNET;
using Relaks;
using Relaks.Utils;

if (OperatingSystem.IsWindows())
{
    Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
    Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
}

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddBlazorDesktop();
builder.Services.AddRelaks();
builder.Services.AddSingleton<IFileProvider>(new CompositeFileProvider(
    new PhysicalFileProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot")),
    RelaksExtensions.FilesProvider()
));
var app = builder.Build();
app.UseRelaks();

var rootComponents = app.Services.GetRequiredService<BlazorWindowRootComponents>();
rootComponents.Add(typeof(Routes), "#app");
rootComponents.Add(typeof(HeadOutlet), "head::after");
var mainWindow = app.Services.GetRequiredService<PhotinoWindow>();
mainWindow
    .SetWidth(1024)
    .SetHeight(768)
    .SetIconFile("favicon.ico")
    .SetTitle("Relaks beta")
    .SetDevToolsEnabled(false)
    ;
mainWindow.Centered = true;

var windowManager = app.Services.GetRequiredService<PhotinoWebViewManager>();
mainWindow.RegisterCustomSchemeHandler(PhotinoWebViewManager.BlazorAppScheme, windowManager.HandleWebRequest);

var appOperation = app.Services.GetRequiredService<AppOperation>();
appOperation.OnRestart += RestartApp;

void RestartApp()
{
    var fileName = Process.GetCurrentProcess().MainModule?.FileName;
    
    // close window = stop process
    mainWindow.Close();
    
    // open new window
    if (!string.IsNullOrEmpty(fileName)) Process.Start(Path.GetFullPath(fileName));
}

AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
{
    mainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
};

await app.StartAsync();
windowManager.Navigate("/");
mainWindow.WaitForClose();
await app.StopAsync();
await app.WaitForShutdownAsync();