// using System.Text.Json.Serialization;
// using App;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
//
// namespace WebApiTests;
//
// public class ApiTestFactory : WebApplicationFactory<Program>
// {
//     protected override IHost CreateHost(IHostBuilder builder)
//     {
//         builder.ConfigureServices(services =>
//         {
//             services.AddScoped(sp =>
//             {
//                 var dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
//                         .UseSqlite("Data Source=C:\\app\\RiderProjects\\Ras\\WebApiTests\\app_test.db")
//                         .UseApplicationServiceProvider(sp)
//                         .Options
//                     ;
//                 
//                 // var db = new ApplicationContext(dbContextOptions);
//                 // db.Database.EnsureDeleted();
//                 // db.Database.EnsureCreated();
//                 // db.Entries.RemoveRange(db.Entries);
//                 // db.SaveChanges();
//
//                 return dbContextOptions;
//             });
//         });
//
//         return base.CreateHost(builder);
//     }
// }