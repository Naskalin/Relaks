// using System;
// using System.Collections.Generic;
// using System.Net.Http;
// using System.Net.Http.Json;
// using System.Threading.Tasks;
// using App;
// using App.Models.Entry;
// using Microsoft.Extensions.DependencyInjection;
// using WebApiTests.Models;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace WebApiTests;
//
// public class ApiTestCase: IClassFixture<ApiTestFactory>
// {
//     public readonly ITestOutputHelper Output;
//     protected readonly ApiTestFactory Factory;
//     // protected readonly ApplicationContext Db;
//     
//     public ApiTestCase(ITestOutputHelper output, ApiTestFactory factory)
//     {
//         Output = output;
//         Factory = factory;
//         
//         using var scope = Factory.Services.CreateScope();
//         using var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
//         db.Entries.RemoveRange(db.Entries);
//         db.SaveChanges();
//     }
//
//     // protected async Task<HttpResponseMessage> CreatePerson(HttpClient client, EntryTestDto? dto = null)
//     protected async Task<HttpResponseMessage> CreatePerson(HttpClient client, object? dto = null)
//     {
//         if (dto == null)
//         {
//             dto = new EntryTestDto()
//             {
//                 Name = "Jerry",
//             };
//         }
//         
//         var resp = await client.PostAsJsonAsync("/api/person", dto);
//         return resp;
//     }
//     
//     protected async Task AddEntries()
//     {
//         using var scope = Factory.Services.CreateScope();
//         var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
//         
//         db.Entries.AddRange(new List<BaseEntry>()
//         {
//             new Person() {Name = "Tom", BirthDay = DateTime.Parse("2000-01-01T00:00:00.00z")},
//             new Person() {Name = "Adam"},
//             new Person() {Name = "Bob"},
//             new Company() {Name = "ABCDE@COMPANY"},
//             new Meet() {Name = "Meet with friends", StartAt = DateTime.UtcNow, EndAt = DateTime.UtcNow, Address = "Moscow city"},
//         });
//
//         await db.SaveChangesAsync();
//     }
//     // protected readonly ApplicationContext Db;
//
//     // protected ApiTestCase()
//     // {
//     //     // await using var application = new ApiTestFactory();
//     //     
//     // var dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
//     //     .UseSqlite("Data Source=C:\\app\\RiderProjects\\Ras\\WebApiTests\\app_test.db")
//     //     // .UseInMemoryDatabase(databaseName: "AppTestDb")
//     //     .Options;
//     // Db = new ApplicationContext(dbContextOptions);
//     // // Db.Database.EnsureDeleted();
//     // Db.Database.EnsureCreated();
//     //
//     // Db.Entries.RemoveRange(Db.Entries);
//     // Db.SaveChanges();
//     // }
// }