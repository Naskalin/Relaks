using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryStructures;

public class ListRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }
    
    [FromQuery]
    public bool? IsDeleted { get; set; }
    
    [FromQuery]
    public DateTime? Date { get; set; }
}