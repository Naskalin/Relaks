// namespace App.Endpoints.Entries.EntryInfos.Phone;
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
//     public RequestPhoneDetails Details { get; set; } = null!;
// }
//
// public class RequestPhoneDetails : IEntryInfoFormCommonRequest
// {
//     public string Title { get; set; } = null!;
//     public DateTime? DeletedAt { get; set; }
//     public string DeletedReason { get; set; } = null!;
//     public string PhoneNumber { get; set; } = null!;
//     public string PhoneRegion { get; set; } = null!;
// }