using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.StructureConnections;

public class ListRequest
{
    [FromQuery]
    public Guid EntryId { get; set; }
    
    [FromQuery]
    public bool? IsDeleted { get; set; }
    
    [FromQuery]
    public DateTime? Date { get; set; }
}