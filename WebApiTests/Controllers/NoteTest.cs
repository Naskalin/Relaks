// using System;
// using System.Linq;
// using System.Net;
// using System.Net.Http;
// using System.Net.Http.Json;
// using System.Text;
// using System.Text.Json;
// using System.Threading.Tasks;
// using App;
// using App.Models;
// using App.Models.Entry;
// using Microsoft.Extensions.DependencyInjection;
// using WebApiTests.Extensions;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace WebApiTests.Controllers;
//
// public class NoteTest : ApiTestCase
// {
//     public NoteTest(ITestOutputHelper output, ApiTestFactory factory) : base(output, factory)
//     {
//     }
//
//     [Fact]
//     public async Task CreateTest()
//     {
//         using var client = Factory.CreateClient();
//         using var scope = Factory.Services.CreateScope();
//         var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
//         
//         await AddEntries();
//
//         var person = db.Persons.FirstOrDefault();
//
//         var resp = await client.PostAsJsonAsync("/api/notes", new
//         {
//             Description = "Привет, мир!",
//             EntryId = person.Id
//         });
//         
//         Output.WriteLine(await resp.Content.ReadAsStringAsync());
//         Assert.Equal(HttpStatusCode.Created, resp.StatusCode);
//     }
//
//     [Fact]
//     public async Task PatchTest()
//     {
//         using var client = Factory.CreateClient();
//         using var scope = Factory.Services.CreateScope();
//         var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
//         
//         await AddEntries();
//
//         var person = db.Persons.FirstOrDefault();
//         var note = new Note()
//         {
//             Description = "My Super Note",
//             EntryId = person.Id
//         };
//         db.Notes.Add(note);
//         await db.SaveChangesAsync();
//
//         // Output.WriteLine(note.Id.ToString());
//
//         // var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
//         var serialize = JsonSerializer.Serialize(new {Description = "Bla bla"});
//         var json = new StringContent(serialize, Encoding.UTF8, "application/json");
//         // var resp = await client.PatchAsync("/api/notes" + note.Id, json);
//         // Output.WriteLine(await resp.Content.ReadAsStringAsync());
//         var resp = await client.PatchAsync("/api/notes" + note.Id, json);
//         Output.WriteLine(await resp.Content.ReadAsStringAsync());
//     }
//
//     // [Fact]
//     // public void OptionalTest()
//     // {
//     //     var note = new NotePatchDto();
//     //     note.Title = null;
//     //     note.Description = "My awesome Description";
//     //
//     //     Output.WriteLine(note.ToString());
//     // }
// }