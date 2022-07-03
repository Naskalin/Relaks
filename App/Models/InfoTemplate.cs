using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace App.Models;

public class InfoTemplate : BaseEntity, ITimestampResource
{
    public string Title { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    [JsonIgnore]
    public string JsonTemplate { get; set; } = null!;

    [NotMapped]
    public CustomInfo Template
    {
        get => JsonSerializer.Deserialize<CustomInfo>(JsonTemplate)!;
        set => JsonTemplate = JsonSerializer.Serialize(value);
    }
}

// public record CustomPassport
// {
//     public enum GenderEnum
//     {
//         Male,
//         Female
//     }
//     
//     public string Fio { get; set; } = null!;
//     public string? DocType { get; set; }
//     public string? DocCode { get; set; }
//     public string? DocNumber { get; set; }
//     public string? Nationality { get; set; }
//
//     public GenderEnum? Gender { get; set; }
//     public string? Birthplace { get; set; }
//     public DateTime? Birthday { get; set; }
//
//     public DateTime? IssueAt { get; set; }
//     public DateTime? ExpireAt { get; set; }
//     public string? PlaceIssue { get; set; }
//     public string? DivisionCode { get; set; }
//     public string? PersonalCode { get; set; }
//     
//     public string? Description { get; set; }
// }
//
// public record CompanyDetails
// {
//     public string? Name { get; set; } = null!;
//     public string? Okved { get; set; } = null!;
//     public string? Inn { get; set; } = null!;
//     public string? Kpp { get; set; } = null!;
//     public string? Okpo { get; set; } = null!;
//     public string? Ogrn { get; set; } = null!;
//     public string? Ogrnip { get; set; } = null!;
//     public string? Oktmo { get; set; } = null!;
//     public string? Address { get; set; } = null!;
//     public DateTime? RegistrationAt { get; set; }
//     public string? Description { get; set; }
// }