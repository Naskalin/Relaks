using System.ComponentModel;
using App.Models;

namespace App.Endpoints.EntryFiles;

public class EntryFilePutRequest : ISoftDelete
{
    public Guid EntryId { get; set; }
    public Guid EntryFileId { get; set; }
    
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public List<string> Tags { get; set; } = new();
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}

public class EntryFileCreateRequest
{
    public Guid EntryId { get; set; }
    // public List<IFormFile> Files { get; set; } = null!;
    public string? Category { get; set; }
}

public class EntryFileGetRequest
{
    public Guid EntryId { get; set; }
    public Guid EntryFileId { get; set; }
}

public class EntryFileListRequest : BaseListRequest
{
    public Guid EntryId { get; set; }
    public Guid? CategoryId { get; set; }
    public List<string> Tags { get; set; } = new();
}