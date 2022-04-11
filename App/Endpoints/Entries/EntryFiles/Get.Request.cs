using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryFiles;

public class GetRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }
    
    [FromRoute]
    public Guid EntryFileId { get; set; }
}