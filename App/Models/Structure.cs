﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace App.Models;

public class Structure : BaseEntity, ISoftDelete, ITimestampResource
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartAt { get; set; }

    public List<StructureItem> Items { get; set; } = new();
    
    [JsonIgnore]
    public Structure? Parent { get; set; }
    public Guid? ParentId { get; set; }
    [JsonIgnore]
    public List<Structure> Children = new();
    
    [JsonIgnore]
    public Entry Entry { get; set; } = null!;
    public Guid EntryId { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class StructureItem : BaseEntity, ISoftDelete, ITimestampResource
{
    public string Description { get; set; } = null!;
    public DateTime StartAt { get; set; }
    
    public Entry Entry { get; set; } = null!;
    public Guid EntryId { get; set; }

    [JsonIgnore]
    public Structure Structure { get; set; } = null!;
    public Guid StructureId { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class StructureConnection : BaseEntity, ITimestampResource, ISoftDelete
{
    public string Description { get; set; } = null!;
    public DirectionEnum Direction { get; set; }
    public Guid StructureFirstId { get; set; }
    public Guid StructureSecondId { get; set; }
    [JsonIgnore]
    public Structure StructureFirst { get; set; } = null!;
    [JsonIgnore]
    public Structure StructureSecond { get; set; } = null!;
    // [JsonIgnore]
    // public string JsonOptions { get; set; } = null!;
    //
    // [NotMapped]
    // public JsonObject Options
    // {
    //     get => JsonSerializer.Deserialize<JsonObject>(JsonOptions)!;
    //     set => JsonOptions = JsonSerializer.Serialize(value);
    // }
    public DateTime StartAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
    
    public enum DirectionEnum
    {
        Normal,
        Reverse,
        Bidirectional
    }
}