using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Relaks.Interfaces;

namespace Relaks.Models;

[Table("EntryInfos")]
public abstract class BaseEntryInfo : IEntryInfo, ITimestamped, ISoftDeletedReason, ICloneable
{
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

    protected BaseEntryInfo()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsFavorite = false;
    }
}

public class EiEmail : BaseEntryInfo, IEmail
{
    public string Email { get; set; } = null!;
}

public class EiPhone : BaseEntryInfo, IPhone
{
    public string Number { get; set; } = null!;
    public string Region { get; set; } = null!;
}

public class EiDate : BaseEntryInfo, IDate
{
    public DateTime Date { get; set; }
    public bool WithTime { get; set; }
}

public class EiUrl : BaseEntryInfo, IUrl
{
    public string Url { get; set; } = null!;
}

public class EiDataset : BaseEntryInfo, IDataset
{
    public string DatasetValue { get; set; } = null!;

    [NotMapped]
    public DatasetModel Dataset
    {
        get => JsonSerializer.Deserialize<DatasetModel>(DatasetValue) ?? new DatasetModel();
        set => DatasetValue = JsonSerializer.Serialize(value);
    }

    public EiDataset()
    {
        Dataset = new DatasetModel();
    }
}

public class EiDatasetRequest : IEntryInfo, IDataset, ISoftDeletedReason
{
    public string? Title { get; set; }
    public bool IsFavorite { get; set; }
    public DatasetModel Dataset { get; set; } = new();
    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
}

public record DatasetItem
{
    public Guid Id { get; set; }
    public string? Key { get; set; }
    public string? Value { get; set; }

    public DatasetItem()
    {
        Id = Guid.NewGuid();
    }
}

public record DatasetGroup
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public List<DatasetItem> Items { get; set; } = new();
    
    public DatasetGroup()
    {
        Id = Guid.NewGuid();
    }
}

public record DatasetModel
{
    public List<DatasetGroup> Groups { get; set; } = new();
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
    public string DeletedAt { get; set; } = null!;
    public string Discriminator { get; set; } = null!;
}