// using System;
// using System.Collections.Generic;
// using System.Net;
// using System.Threading.Tasks;
// using App;
// using App.Models.Entry;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace WebApiTests.Controllers;
//
// public class EntryTest : ApiTestCase
// {
//     public EntryTest(ITestOutputHelper output, ApiTestFactory factory) : base(output, factory)
//     {
//     }
//     
//     [Fact]
//     public async Task GetAll()
//     {
//         using var client = Factory.CreateClient();
//         await AddEntries();
//         var resp = await client.GetAsync("/api/Person");
//         // Output.WriteLine(await resp.Content.ReadAsStringAsync());
//         Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
//         
//         // resp = await client.GetAsync("/api/Person?$select=name&$count=2$skip=1&$top=2");
//         resp = await client.GetAsync("/api/Person?$filter=birthday ge 2000-01-01T00:00:00.00z");
//         Output.WriteLine(await resp.Content.ReadAsStringAsync());
//         // Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
//     }
//
//     [Fact]
//     public async void GetSingle()
//     {
//         using var client = Factory.CreateClient();
//         using var scope = Factory.Services.CreateScope();
//         var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
//         await AddEntries();
//         var person = await db.Persons.FirstAsync();
//         // Output.WriteLine(person.Id.ToString());
//         var resp = await client.GetAsync("/api/Person/" + person.Id);
//         Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
//         Output.WriteLine(await resp.Content.ReadAsStringAsync());
//     }
//
//     [Fact]
//     public async Task DeleteEntry()
//     {
//         using var client = Factory.CreateClient();
//         using var scope = Factory.Services.CreateScope();
//         var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
//
//         await AddEntries();
//         var person = await db.Persons.FirstAsync();
//         int count = await db.Persons.CountAsync();
//
//         var resp = await client.DeleteAsync("/api/Person/" + person.Id);
//         Assert.Equal(HttpStatusCode.NoContent, resp.StatusCode);
//         Assert.Equal(count - 1, await db.Persons.CountAsync());
//     }
// }