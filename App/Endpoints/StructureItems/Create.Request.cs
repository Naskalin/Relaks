using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.StructureItems;

public class CreateRequest
{
    [FromBody]
    public StructureItemFormDetails Details { get; set; } = null!;
}

public class StructureItemFormDetails : ISoftDelete
{
    public Guid StructureId { get; set; }
    public Guid EntryId { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime? StartAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}