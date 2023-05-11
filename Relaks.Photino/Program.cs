using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Photino.Blazor;
using PhotinoNET;
using Relaks;

if (OperatingSystem.IsWindows())
{
    Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
    Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
}

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddBlazorDesktop();
builder.Services.AddRelaks();

var app = builder.Build();
app.UseRelaks();

var rootComponents = app.Services.GetRequiredService<BlazorWindowRootComponents>();
rootComponents.Add(typeof(App), "#app");
var mainWindow = app.Services.GetRequiredService<PhotinoWindow>();
mainWindow
    .SetWidth(1024)
    .SetHeight(768)
    .SetIconFile("favicon.ico")
    .SetTitle("Relaks beta")
    .SetDevToolsEnabled(true)
    ;
mainWindow.Centered = true;


var windowManager = app.Services.GetRequiredService<PhotinoWebViewManager>();
mainWindow.RegisterCustomSchemeHandler(PhotinoWebViewManager.BlazorAppScheme, windowManager.HandleWebRequest);

AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
{
    mainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
};

await app.StartAsync();
windowManager.Navigate("/");
mainWindow.WaitForClose();
await app.StopAsync();
await app.WaitForShutdownAsync();