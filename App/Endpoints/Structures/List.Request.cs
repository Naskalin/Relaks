using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Structures;

public class ListRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }
    
    [FromQuery]
    public bool? IsDeleted { get; set; }
    
    [FromQuery]
    public DateTime? Date { get; set; }
    
    [FromQuery]
    public Boolean IsTree { get; set; }
}