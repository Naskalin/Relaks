using App.Models;

namespace App.Endpoints;

public interface IPaginableRequest
{
    public int? Page { get; set; }
    public int? PerPage { get; set; }
}

public interface ISearchableRequest
{
    public string? Search { get; set; }
}

public interface ISortableRequest
{
    public string? OrderBy { get; set; }
    public bool? OrderByDesc { get; set; }
}