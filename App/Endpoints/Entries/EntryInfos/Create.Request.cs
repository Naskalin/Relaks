// using System.ComponentModel.DataAnnotations;
// using Microsoft.AspNetCore.Mvc;
//
// namespace App.Endpoints.Entries.EntryInfos;
//
// public class CreateRequest
// {
//     [FromRoute]
//     [Required]
//     public Guid EntryId { get; set; }
//
//     [FromBody]
//     public EntryInfoDetails Details { get; set; } = null!;
// }
//
// public class EntryInfoDetails
// {
//     public string Title { get; set; } = null!;
//     public DateTime? DeletedAt { get; set; }
//     public string DeletedReason { get; set; } = null!;
//     
//     public string Discriminator { get; set; } = null!;
//     public string? Note { get; set; } = null!;
//     public string? Phone { get; set; } = null!;
//     public string? PhoneRegion { get; set; } = null!;
//     public string? Email { get; set; } = null!;
//     public string? Url { get; set; } = null!;
//     public DateTime? Date { get; set; }
// }