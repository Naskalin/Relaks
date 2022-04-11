namespace App.Models;

public class FileModel : BaseEntity, ITimestampResource, ISoftDelete
{
    public string Path { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public string Name { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}

public class EntryFile : FileModel
{
    public Guid EntryId { get; set; }
    
    public string GetFilePath()
    {
        return System.IO.Path.Combine(EntryId.ToString(), Path);
    }
}

public class EntryInfoFile : FileModel
{
    public Guid EntryInfoId { get; set; }
}