using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Relaks.Interfaces;

namespace Relaks.Models;

public abstract class EntryInfo : ITimestamped, ISoftDeleted, ICloneable
{
    // public const string Phone = "PHONE";
    // public const string Email = "EMAIL";
    // public const string Url = "URL";
    // public const string Note = "NOTE";
    // public const string Date = "DATE";
    // public const string Custom = "CUSTOM";
    
    public Guid Id { get; set; }
    public Guid EntryId { get; set; }
    public Entry Entry { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Title { get; set; }
    public bool IsFavorite { get; set; }
    public string Discriminator { get; set; } = null!;

    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public EntryInfo()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsFavorite = false;
    }
}

public class EiEmail : EntryInfo
{
    public string Email { get; set; } = null!;
}

public class EiPhone : EntryInfo, IPhone
{
    public string Number { get; set; } = null!;
    public string Region { get; set; } = null!;
}

public class EiDate : EntryInfo
{
    public DateTime Date { get; set; }
}

public class EiUrl : EntryInfo
{
    public string Url { get; set; } = null!;
}

public class EiCustom : EntryInfo
{
    public string CustomValue { get; set; } = null!;
    
    [NotMapped]
    public CustomInfo Custom
    {
        get => JsonSerializer.Deserialize<CustomInfo>(CustomValue) ?? new CustomInfo();
        set => CustomValue = JsonSerializer.Serialize(value);
    }
}

// public class FtsEntryInfo : IFtsEntity
// {
//     public int RowId { get; set; }
//     public string Match { get; set; } = null!;
//     public double? Rank { get; set; }
//
//     public Guid Id { get; set; }
//     public Guid EntryId { get; set; }
//
//     public string Data { get; set; } = null!;
// }

// public record NoteInfo
// {
//     public string Note { get; set; } = null!;
// }

// public record PhoneInfo
// {
//     public string Number { get; set; } = null!;
//     public string Region { get; set; } = null!;
//
//     public override string ToString()
//     {
//         return Region + "|" + Number;
//     }
// }

// public record EmailInfo
// {
//     public string Email { get; set; } = null!;
// }

// public record UrlInfo
// {
//     public string Url { get; set; } = null!;
// }

// public record DateInfo
// {
//     public DateTime Date { get; set; }
// }

public record CustomInfoItem
{
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
}

public record CustomInfoGroup
{
    public string Title { get; set; } = null!;
    public List<CustomInfoItem> Items { get; set; } = new();
}

public record CustomInfo
{
    public List<CustomInfoGroup> Groups { get; set; } = new();
}