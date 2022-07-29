using App.Models;

namespace App.Endpoints.StructureConnections;

public class StructureConnectionFormRequest : ISoftDelete
{
    public string Description { get; set; } = null!;
    public StructureConnection.DirectionEnum Direction { get; set; }
    public Guid StructureFirstId { get; set; }
    public Guid StructureSecondId { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}

public class StructureConnectionCreateRequest : StructureConnectionFormRequest
{
    
}

public class StructureConnectionPutRequest : StructureConnectionFormRequest
{
    public Guid StructureConnectionId { get; set; }
}

public class StructureConnectionGetRequest
{
    public Guid StructureConnectionId { get; set; }
}

public class StructureConnectionListRequest
{
    public Guid EntryId { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? Date { get; set; }
}