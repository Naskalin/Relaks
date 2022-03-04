// using System.ComponentModel.DataAnnotations;
//
// namespace App.Models.Entry;
//
// public abstract record BaseEntryDto
// {
//     [MinLength(1)]
//     public string Name { get; set; } = null!;
//     
//     [Range(0, 10)]
//     public int? Reputation { get; set; }
// }
//     
// public record MeetDto : BaseEntryDto
// {
//     public DateTime StartAt { get; set; }
//     public DateTime EndAt { get; set; }
//     
//     [MinLength(1), MaxLength(1000)]
//     public string Address { get; set; } = null!;
// }
//     
// public record PersonDto : BaseEntryDto
// {
//     public DateTime? BirthDay { get; set; }
// }
//     
// public record CompanyDto : BaseEntryDto
// {
//     
// }