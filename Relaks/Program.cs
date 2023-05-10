// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Relaks.Database;
// using Relaks.Managers;
//
// namespace Relaks;
//
// public class Program
// {
//     public static void Main(string[] args)
//         => CreateHostBuilder(args).Build().Run();
//         
//     /// <summary>
//     /// https://learn.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#from-application-services
//     /// </summary>
//     public static IHostBuilder CreateHostBuilder(string[] args)
//         => Host.CreateDefaultBuilder(args)
//             .ConfigureWebHostDefaults(
//                 webBuilder => webBuilder.UseStartup<MigrationStartup>());
// }
//
// public class MigrationStartup
// {
//     public void ConfigureServices(IServiceCollection services)
//     {
//         var projectDir = AppDomain.CurrentDomain.BaseDirectory;
//         var relaksConfig = RelaksConfigManager.GetOrCreateConfig(projectDir);
//         services.AddDbContext<AppDbContext>(o =>
//             o.UseSqlite(relaksConfig.SqliteConnectionString())
//         );
//     }
//
//     public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//     {
//         
//     }
// }