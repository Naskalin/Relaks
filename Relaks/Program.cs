using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Photino.Blazor;
using Relaks.Database;
using Relaks.Database.Seeders;
using Relaks.Managers;

namespace Relaks;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var builder = PhotinoBlazorAppBuilder.CreateDefault(args);

        var relaksConfig = GetRelaksConfig.Get();
        builder.Services.AddSingleton(relaksConfig);

        builder.Services.AddDbContext<AppDbContext>(o =>
            o.UseSqlite(relaksConfig.SqliteConnectionString())
        );

        builder.Services.AddLogging();
        builder.Services.RegisterManagers();
        builder.Services.AddBlazorise(o => { o.Immediate = true; })
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
        // register root component and selector
        builder.RootComponents.Add<App>("#app");

        var app = builder.Build();

        // var env = app.Services.GetService<IWebHostEnvironment>();
        // var isDev = env?.IsDevelopment() ?? false;

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();

        new DatabaseSeeder(db).SeedAll();

        app.MainWindow
            .SetWidth(1024)
            .SetHeight(768)
            .SetIconFile("favicon.ico")
            .SetTitle("Relaks beta");

        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
        {
            app.MainWindow.OpenAlertWindow("Fatal exception", error.ExceptionObject.ToString());
        };

        app.Run();
    }

    /// <summary>
    /// https://learn.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#from-application-services
    ///
    /// Это подключение базы данных используется при миграции, в противном случае "окно" блокирует дальнейший вызов.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<MigrationStartup>());
}

public class MigrationStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var relaksConfig = GetRelaksConfig.Get();
        services.AddDbContext<AppDbContext>(o =>
            o.UseSqlite(relaksConfig.SqliteConnectionString())
        );
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
}

public static class GetRelaksConfig
{
    public static RelaksConfigManager.RelaksConfigModel Get()
    {
        var projectDir = Directory.GetCurrentDirectory();
        return RelaksConfigManager.GetConfig(projectDir);
    }
}