using App.Models;

namespace App.Endpoints;

public interface IListRequest : IPaginableRequest, ISearchableRequest, IOrderableRequest
{
}

public interface IPaginableRequest
{
    public int? Page { get; set; }
    public int? PerPage { get; set; }
}

public interface ISearchableRequest
{
    public string? Search { get; set; }
}

public interface IOrderableRequest
{
    public string? OrderBy { get; set; }
    public bool? OrderByDesc { get; set; }
}

public interface ISoftDeleteRequest
{
    public bool? IsFullDelete { get; set; }
    public string DeletedReason { get; set; }
}

// public interface ISoftDeletableRequest
// {
//     public string DeletedReason { get; set; }   
// }