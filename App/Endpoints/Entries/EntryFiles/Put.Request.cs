using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryFiles;

public class PutRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }
    
    [FromRoute]
    public Guid EntryFileId { get; set; }

    [FromBody]
    public EntryFilePutDetails Details { get; set; } = null!;
}

public class EntryFilePutDetails : ISoftDelete
{
    public string Name { get; set; } = null!;
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}