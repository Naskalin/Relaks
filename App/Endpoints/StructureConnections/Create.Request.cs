using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.StructureConnections;

public class CreateRequest
{
    [FromBody]
    public StructureConnectionFormDetails Details { get; set; } = null!;
}

public class StructureConnectionFormDetails
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public StructureConnection.DirectionEnum Direction { get; set; }
    public Guid StructureFirstId { get; set; }
    public Guid StructureSecondId { get; set; }
}