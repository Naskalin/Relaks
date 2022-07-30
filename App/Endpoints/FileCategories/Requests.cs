using App.Models;

namespace App.Endpoints.FileCategories;

public class FileCategoryFormRequest : ISoftDelete
{
    public string Title { get; set; } = null!;
    public Guid EntryId { get; set; }
    public Guid? ParentId { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}

public class FileCategoryCreateRequest : FileCategoryFormRequest
{
}

public class FileCategoryPutRequest : FileCategoryFormRequest
{
    public Guid FileCategoryId { get; set; }
}

public class FileCategoryGetRequest
{
    public Guid FileCategoryId { get; set; }
}

public class FileCategoryListRequest : ISoftDeleteRequest
{
    public Guid EntryId { get; set; }
    public bool? IsTree { get; set; }
    public bool? IsDeleted { get; set; }
}