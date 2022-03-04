// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Net;
// using System.Threading.Tasks;
// using WebApiTests.Models;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace WebApiTests.Controllers;
//
// public class EntryCreateTest : ApiTestCase
// {
//     public EntryCreateTest(ITestOutputHelper output, ApiTestFactory factory) : base(output, factory)
//     {
//     }
//     
//     [Theory, ClassData(typeof(OkData))]
//     public async Task OkTest(object data)
//     {
//         using var client = Factory.CreateClient();
//         var resp = await CreatePerson(client, data);
//         Output.WriteLine(await resp.Content.ReadAsStringAsync());
//         
//         Assert.Equal(HttpStatusCode.Created, resp.StatusCode);
//     }
//     
//     [Theory, ClassData(typeof(ErrorsData))]
//     public async Task ErrorTest(object data)
//     {
//         using var client = Factory.CreateClient();
//         var resp = await CreatePerson(client, data);
//         Output.WriteLine(await resp.Content.ReadAsStringAsync());
//         Assert.Equal(HttpStatusCode.BadRequest, resp.StatusCode);
//     }
// }
//
// public class OkData : IEnumerable<object[]>
// {
//     public IEnumerator<object[]> GetEnumerator()
//     {
//         yield return new object[] {new EntryTestDto() {Name = "A"}};
//         yield return new object[] {new EntryTestDto() {Name = "Tom", Reputation = 0}};
//         yield return new object[] {new EntryTestDto() {Name = "Adam", Reputation = 10}};
//         yield return new object[] {new EntryTestDto() {Name = "Jerry", BirthDay = DateTime.UtcNow}};
//     }
//
//     IEnumerator IEnumerable.GetEnumerator()
//     {
//         return GetEnumerator();
//     }
// }
//
// public class ErrorsData : IEnumerable<object[]>
// {
//     public IEnumerator<object[]> GetEnumerator()
//     {
//         yield return new object[] {new EntryTestDto() {Name = "Tom", Reputation = -1}};
//         yield return new object[] {new EntryTestDto() {Name = "Tom", Reputation = 11}};
//         yield return new object[] {new EntryTestDto() {}};
//         yield return new object[] {new EntryTestDto() {Name = null}};
//         yield return new object[] {new EntryTestDto() {Name = ""}};
//     }
//
//     IEnumerator IEnumerable.GetEnumerator()
//     {
//         return GetEnumerator();
//     }
// }