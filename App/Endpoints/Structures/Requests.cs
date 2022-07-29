using App.Models;

namespace App.Endpoints.Structures;

public class StructureFormRequest : ISoftDelete
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime? StartAt { get; set; }
    public Guid? ParentId { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}

public class StructureCreateRequest : StructureFormRequest
{
    public Guid EntryId { get; set; }
}

public class StructurePutRequest : StructureFormRequest
{
    public Guid EntryId { get; set; }
    public Guid StructureId { get; set; }
}

public class StructureListRequest
{
    public Guid EntryId { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? Date { get; set; }
    public Boolean? IsTree { get; set; }
}

public class StructureGetRequest
{
    public Guid EntryId { get; set; }
    public Guid StructureId { get; set; }
}