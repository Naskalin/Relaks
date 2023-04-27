﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Relaks.Interfaces;

namespace Relaks.Models;

[Table("EntryInfos")]
public abstract class BaseEntryInfo : ITimestamped, ISoftDeleted, ICloneable
{
    // public const string Phone = "PHONE";
    // public const string Email = "EMAIL";
    // public const string Url = "URL";
    // public const string Note = "NOTE";
    // public const string Date = "DATE";
    // public const string Custom = "CUSTOM";

    public Guid Id { get; set; }
    public BaseEntry Entry { get; set; } = null!;
    public Guid EntryId { get; set; }
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

    public BaseEntryInfo()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsFavorite = false;
    }
}

public class EiEmail : BaseEntryInfo
{
    public string Email { get; set; } = null!;
}

public class EiPhone : BaseEntryInfo, IPhone
{
    public string Number { get; set; } = null!;
    public string Region { get; set; } = null!;
}

public class EiDate : BaseEntryInfo
{
    public DateTime Date { get; set; }
    public bool IsOnlyDate { get; set; }

    // public DateTime ToTz(string timeZoneId)
    // {
    //     return TimeZoneInfo.ConvertTimeToUtc(Date, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
    // }
}

public class EiUrl : BaseEntryInfo
{
    public string Url { get; set; } = null!;
}

public class EiCustom : BaseEntryInfo
{
    public string CustomValue { get; set; } = null!;

    [NotMapped]
    public CustomInfo Custom
    {
        get => JsonSerializer.Deserialize<CustomInfo>(CustomValue) ?? new CustomInfo();
        set => CustomValue = JsonSerializer.Serialize(value);
    }
}

public class FtsEntryInfo : IFtsEntity
{
    public int RowId { get; set; }
    public string Match { get; set; } = null!;
    public string Snippet { get; set; } = null!;
    public double? Rank { get; set; }

    public Guid Id { get; set; }
    public string Body { get; set; } = null!;
    public Guid EntryId { get; set; }
}

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