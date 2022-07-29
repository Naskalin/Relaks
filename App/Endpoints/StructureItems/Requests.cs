using App.Models;

namespace App.Endpoints.StructureItems;

public class StructureItemGetRequest
{
    public Guid StructureItemId { get; set; }
}
public class StructureItemCreateRequest : StructureItemFormRequest { }
public class StructureItemPutRequest : StructureItemFormRequest
{
    public Guid StructureItemId { get; set; }
}

public class StructureItemFormRequest : ISoftDelete
{
    public Guid StructureId { get; set; }
    public Guid EntryId { get; set; }
    public string Description { get; set; } = null!;
    public DateTime? StartAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}

public class StructureItemListRequest : IPaginableRequest 
{
    public Guid? StructureId { get; set; }
    public Guid? EntryId { get; set; }
    public EntryTypeEnum? EntryType { get; set; }
    public bool? IsDeleted { get; set; }
    public int? Page { get; set; }
    public int? PerPage { get; set; }
    public DateTime? Date { get; set; }
}