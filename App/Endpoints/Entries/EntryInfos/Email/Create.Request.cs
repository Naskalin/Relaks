// using App.Models;
//
// namespace App.Endpoints.Entries.EntryInfos.Email;
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
//     public RequestEmailDetails Details { get; set; } = null!;
//     // public EntryEmailCreateRequestDetails Details { get; set; } = null!;
// }
//
// public class RequestEmailDetails : IEntryInfoFormCommonRequest
// {
//     public string Title { get; set; } = null!;
//     public DateTime? DeletedAt { get; set; }
//     public string DeletedReason { get; set; } = null!;
//     
//     public InfoEmail Value { get; set; } = null!;
// }