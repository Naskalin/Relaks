﻿namespace App.Models;

public class FileModel : BaseEntity, ITimestampResource, ISoftDelete
{
    public string Path { get; set; } = null!;
    public string MimeType { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}

public class EntryFile : FileModel
{
    public Guid EntryId { get; set; }
}

public class EntryInfoFile : FileModel
{
    public Guid EntryInfoId { get; set; }
}