// using Microsoft.AspNetCore.Mvc;
//
// namespace App.Endpoints.Entries.EntryInfos;
//
// public class EntryInfoDeleteRequest : EntryInfoGetRequest, ISoftDeleteRequest
// {
//     [FromBody]
//     public bool? IsFullDelete { get; set; }
//
//     [FromBody]
//     public string DeletedReason { get; set; } = null!;
// }