using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints;

public class BaseListRequest : IPaginableRequest, ISearchableRequest, ISortableRequest
{
    [FromQuery] public int? Page { get; set; }
    [FromQuery] public int? PerPage { get; set; }
    [FromQuery] public string? Search { get; set; }
    [FromQuery] public string? OrderBy { get; set; }
    [FromQuery] public bool? OrderByDesc { get; set; }
}