// using System;
// using System.Linq;
// using System.Net;
// using System.Net.Http;
// using System.Net.Http.Json;
// using System.Text;
// using System.Text.Json;
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
// public class EntryPatchTest : ApiTestCase
// {
//     public EntryPatchTest(ITestOutputHelper output, ApiTestFactory factory) : base(output, factory)
//     {
//     }
//     
//    
//     // [Fact]
//     // public async Task PatchOk()
//     // {
//     //     using var client = Factory.CreateClient();
//     //     using var scope = Factory.Services.CreateScope();
//     //     var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
//     //     await AddEntries();
//     //     var person = await db.Persons.FirstAsync();
//     //     Output.WriteLine(person.Name);
//     //
//     //     var obj = new {Name = "New Name"};
//     //     var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
//     //     var serialize = JsonSerializer.Serialize(obj, jsonSerializerOptions);
//     //     // var json = new StringContent(serialize, Encoding.UTF8, "application/json");
//     //     // var resp = await client.PatchAsync("/api/Person/" + person.Id, json);
//     //     // var deserialize = JsonSerializer.Deserialize<Delta<Person>>(serialize, jsonSerializerOptions);
//     //     
//     //     var resp = await client.PutAsJsonAsync("/api/Person/" + person.Id, obj);
//     //     Assert.Equal(HttpStatusCode.NoContent, resp.StatusCode);
//     //     
//     //     var patchedPerson = await db.Persons.FirstOrDefaultAsync(x => x.Id == person.Id);
//     //     Output.WriteLine(patchedPerson.Name);
//     //     // Assert.NotEqual(person.Name, patchedPerson.Name);
//     // }
// }