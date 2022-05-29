using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using App.Utils;

namespace App.Models;

public class EntryInfo : BaseEntity, ITimestampResource, ISoftDelete, IInfoData
{
    // public const string PhoneType = "Phone";
    // public const string EmailType = "Email";
    // public const string UrlType = "Url";
    // public const string NoteType = "Note";
    // public const string DateType = "Date";
    // public const string PassportType = "Passport";
    // public const string CompanyDetailsType = "CompanyDetails";

    public Guid EntryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Title { get; set; } = null!;

    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;

    public List<EntryInfoFile> Files { get; set; } = new();

    public EntryInfoType Type { get; set; }

    [JsonIgnore]
    public string Value { get; set; } = null!;

    [NotMapped]
    // Для сериализации в json вместо Value, иначе мы получаем строку в Value: "{...}"
    public JsonObject Data => JsonSerializer.Deserialize<JsonObject>(Value, InfoValue.WriteOptions)!;
}

public enum EntryInfoType
{
    Phone,
    Email,
    Url,
    Note,
    Date,
    Passport,
    CompanyDetails,
    Custom,
}

public class EntryInfoFts : IFtsEntity
{
    public int RowId { get; set; }
    public string Match { get; set; } = null!;
    public double? Rank { get; set; }

    public Guid Id { get; set; }
    public Guid EntryId { get; set; }

    public string Title { get; set; } = null!;
    public string DeletedReason { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Url { get; set; }
    public string? Note { get; set; }
}

public class NoteInfo
{
    public string Note { get; set; } = null!;
}
//
// public class InfoPhone : EntryInfo
// {
//     public string PhoneNumber { get; set; } = null!;
//     public string PhoneRegion { get; set; } = null!;
// }

public class PhoneInfo
{
    public string Number { get; set; } = null!;
    public string Region { get; set; } = null!;

    public override string ToString()
    {
        return Region + "|" + Number;
    }
}
public class EmailInfo
{
    public string Email { get; set; } = null!;
}
public class UrlInfo
{
    public string Url { get; set; } = null!;
}

public class DateInfo
{
    public DateTime Date { get; set; }
}

// public enum Gender
// {
//     Male,
//     Female
// }
// public record InfoPassport
// {
//     public string? DocType { get; set; }
//     public string? DocCode { get; set; }
//     public string? DocNumber { get; set; }
//     public string? Nationality { get; set; }
//     
//     public string? Fio { get; set; } = null!;
//     public Gender? Gender { get; set; }
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
// public record InfoCompanyDetails
// {
//     public string? Name { get; set; } = null!;
//     public string? Inn { get; set; } = null!;
//     public string? Okpo { get; set; } = null!;
//     public string? Ogrnip { get; set; } = null!;
//     public string? Oktmo { get; set; } = null!;
//     public string? Address { get; set; } = null!;
//     public DateTime? RegistrationAt { get; set; }
//     public string? Description { get; set; }
// }