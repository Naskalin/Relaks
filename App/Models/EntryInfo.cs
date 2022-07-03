using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using App.Utils;

namespace App.Models;

public class EntryInfo : BaseEntity, ITimestampResource, ISoftDelete, IInfoData, ICloneable
{
    public const string Phone = "PHONE";
    public const string Email = "EMAIL";
    public const string Url = "URL";
    public const string Note = "NOTE";
    public const string Date = "DATE";
    public const string Custom = "CUSTOM";
    
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