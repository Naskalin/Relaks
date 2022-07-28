using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.StructureConnections;

public class CreateRequest
{
    public StructureConnectionFormDetails Details { get; set; } = null!;
}

public class StructureConnectionFormDetails : ISoftDelete
{
    public string Description { get; set; } = null!;
    public StructureConnection.DirectionEnum Direction { get; set; }
    public Guid StructureFirstId { get; set; }
    public Guid StructureSecondId { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}