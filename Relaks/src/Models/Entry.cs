﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Relaks.Interfaces;

namespace Relaks.Models;

[Table("Entries")]
public abstract class BaseEntry : IEntry, ITimestamped, ISoftDeletedReason
{
    public Guid Id { get; set; }
    [StringLength(50)]
    public string Discriminator { get; set; } = null!;
    
    [StringLength(255)]
    public string Name { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public bool StartAtWithTime { get; set; }
    public bool EndAtWithTime { get; set; }

    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
    public string? Thumbnail { get; set; }
    
    public List<BaseEntryInfo> EntryInfos { get; set; } = new();

    protected BaseEntry()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Name = "";
    }
}

public class EPerson : BaseEntry
{
    
}

public class ECompany : BaseEntry
{
    
}

public class EMeet : BaseEntry
{
    
}

public class FtsEntry : IFtsEntity
{
    public int RowId { get; set; }
    public string Match { get; set; } = null!;
    public string Snippet { get; set; } = null!;
    public double? Rank { get; set; }
    
    public Guid Id { get; set; }
    public string Body { get; set; } = null!;

    public string DeletedAt { get; set; } = null!;
    public string Discriminator { get; set; } = null!;
}