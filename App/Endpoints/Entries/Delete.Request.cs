// using Microsoft.AspNetCore.Mvc;
//
// namespace App.Endpoints.Entries;
//
// public class DeleteRequest : ISoftDeleteRequest
// {
//     [FromRoute]
//     public Guid EntryId { get; set; }
//
//     [FromBody]
//     public bool? IsFullDelete { get; set; }
//
//     [FromBody]
//     public string DeletedReason { get; set; }
// }