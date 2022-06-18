using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.StructureItems;

public class ListRequest : IPaginableRequest 
{
    [FromQuery]
    public Guid? StructureId { get; set; }
    
    [FromQuery]
    public Guid? EntryId { get; set; }
    
    [FromQuery]
    public EntryTypeEnum? EntryType { get; set; }
    
    [FromQuery]
    public bool? IsDeleted { get; set; }

    [FromQuery]
    public int? Page { get; set; }

    [FromQuery]
    public int? PerPage { get; set; }
}