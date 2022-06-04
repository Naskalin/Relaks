using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using App.Utils;

namespace App.Models;

public class EntryInfo : BaseEntity, ITimestampResource, ISoftDelete, IInfoData, ICloneable
{
    public Guid EntryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Title { get; set; } = null!;

    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;

    public List<EntryInfoFile> Files { get; set; } = new();

    public string Type { get; set; } = null!;

    [JsonIgnore]
    public string Value { get; set; } = null!;

    [NotMapped]
    // Для сериализации в json вместо Value, иначе мы получаем строку в Value: "{...}"
    public JsonObject Info => JsonSerializer.Deserialize<JsonObject>(Value)!;

    public object Clone()
    {
        return MemberwiseClone();
    }
}

// public enum EntryInfoType
// {
//     Phone,
//     Email,
//     Url,
//     Note,
//     Date,
//     Passport,
//     CompanyDetails,
//     Custom,
// }

public class EntryInfoType : InfoBaseType
{
    public static readonly InfoBaseType Passport = new("Passport");
    public static readonly InfoBaseType CompanyDetails = new("CompanyDetails");
    public static readonly InfoBaseType Custom = new("Custom");

    public EntryInfoType(string value) : base(value)
    {
    }
}

public class InfoBaseType
{
    public static readonly InfoBaseType Phone = new("Phone");
    public static readonly InfoBaseType Email = new("Email");
    public static readonly InfoBaseType Url = new("Url");
    public static readonly InfoBaseType Note = new("Note");
    public static readonly InfoBaseType Date = new("Date");
    public string Value { get; protected set; }

    public override string ToString() => Value.ToUpper();
    
    public bool Equals(string s) => s.ToUpper() == Value.ToUpper();
    public bool Equals(InfoBaseType other) => Value.ToUpper() == other.Value.ToUpper();

    public InfoBaseType(string value)
    {
        Value = value;
    }
}

public class FtsEntryInfo : IFtsEntity
{
    public int RowId { get; set; }
    public string Match { get; set; } = null!;
    public double? Rank { get; set; }

    public Guid Id { get; set; }
    public Guid EntryId { get; set; }

    public string Data { get; set; } = null!;
}

public record NoteInfo
{
    public string Note { get; set; } = null!;
}

public record PhoneInfo
{
    public string Number { get; set; } = null!;
    public string Region { get; set; } = null!;

    public override string ToString()
    {
        return Region + "|" + Number;
    }
}

public record EmailInfo
{
    public string Email { get; set; } = null!;
}

public record UrlInfo
{
    public string Url { get; set; } = null!;
}

public record DateInfo
{
    public DateTime Date { get; set; }
}

// public enum PassportGender
// {
//     Male,
//     Female
// }

public enum CustomInfoType
{
    Phone,
    Email,
    Url,
    Note,
    Date
}

public record CustomItem
{
    public string Key { get; set; } = null!;
    public CustomInfoType Type { get; set; }
    public string Val { get; set; } = null!;
}

public record CustomInfo
{
    public List<CustomItem> Items { get; set; } = new();
}

// public record PassportInfo
// {
//     public string? DocType { get; set; }
//     public string? DocCode { get; set; }
//     public string? DocNumber { get; set; }
//     public string? Nationality { get; set; }
//
//     public string? Fio { get; set; } = null!;
//     public PassportGender? Gender { get; set; }
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
// public record CompanyDetailsInfo
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