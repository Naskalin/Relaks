using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Relaks.Managers;

namespace Relaks.Database;

/// <summary>
/// Используется в качестве способа получения контекста базы данных при миграциях
/// https://learn.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#from-a-design-time-factory
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var projectDir = AppDomain.CurrentDomain.BaseDirectory;
        var relaksConfig = RelaksConfigManager.GetOrCreateConfig(projectDir);
        
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite(relaksConfig.SqliteConnectionString());

        return new AppDbContext(optionsBuilder.Options);
    }
}