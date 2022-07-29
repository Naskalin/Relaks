namespace App.Models;

public class FileCategory : BaseEntity, ITimestampResource
{
    public string Title { get; set; } = null!;
    public string Discriminator { get; set; } = null!;
    public FileCategory? Parent { get; set; }
    public Guid? ParentId { get; set; }
    public List<FileCategory> Children = new();
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class EntryFileCategory : FileCategory
{
    public Guid EntryId { get; set; }
    public Entry Entry { get; set; } = null!;
}