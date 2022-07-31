using Newtonsoft.Json;

namespace App.Models;

public class FileCategory : BaseEntity, ITimestampResource, ISoftDelete
{
    public string Title { get; set; } = null!;
    public string Discriminator { get; set; } = null!;
    
    [JsonIgnore]
    public FileCategory? Parent { get; set; }
    public Guid? ParentId { get; set; }
    
    [JsonIgnore]
    public List<FileCategory> Children = new();
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}

public class EntryFileCategory : FileCategory
{
    public Guid EntryId { get; set; }
    [JsonIgnore]
    public Entry Entry { get; set; } = null!;
}