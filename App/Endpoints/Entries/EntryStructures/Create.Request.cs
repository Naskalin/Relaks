using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryStructures;

public class CreateRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }

    [FromBody]
    public CreateStructureRequestDetails Details { get; set; } = null!;
}

public class CreateStructureRequestDetails : ISoftDelete
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartAt { get; set; }
    public Guid? ParentId { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
    
}