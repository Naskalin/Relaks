using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.StructureConnections;

public class ListRequest
{
    [FromQuery]
    public Guid EntryId { get; set; }
}