// namespace App.Endpoints.Entries.EntryInfos.Note;
//
// using System.ComponentModel.DataAnnotations;
// using Microsoft.AspNetCore.Mvc;
//
// public class CreateRequest
// {
//     [FromRoute]
//     [Required]
//     public Guid EntryId { get; set; }
//
//     [FromBody]
//     public RequestNoteDetails Details { get; set; } = null!;
// }
//
// public class RequestNoteDetails : IEntryInfoFormCommonRequest
// {
//     public string Title { get; set; } = null!;
//     public DateTime? DeletedAt { get; set; }
//     public string DeletedReason { get; set; } = null!;
//     public string Note { get; set; } = null!;
// }