using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Relaks.Database;
using Relaks.Managers;

namespace Relaks;

public static class RelaksExtensions 
{
    public static void AddRelaks(this IServiceCollection services)
    {
        var projectDir = AppDomain.CurrentDomain.BaseDirectory;
        var relaksConfig = RelaksConfigManager.GetOrCreateConfig(projectDir);
        services.AddSingleton(relaksConfig);

        services.AddDbContext<AppDbContext>(o =>
            o.UseSqlite(relaksConfig.SqliteConnectionString())
        );

        services.RegisterValidators();
        services.RegisterManagers();
        services.RegisterLocalization();
        services.AddBlazoredLocalStorage();
        services.AddBootstrapBlazor();
    }

    public static void UseRelaks(this IHost host)
    {
        var env = host.Services.GetRequiredService<IHostEnvironment>();
        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        
        if (env.IsDevelopment())
        {   
            // new Database.Seeders.DatabaseSeeder(db).SeedAll();
        }
    }
}