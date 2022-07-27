using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints;

public class BaseListRequest : IListRequest
{
    public int? Page { get; set; }
    public int? PerPage { get; set; }

    public string? Search { get; set; }
    
    public string? OrderBy { get; set; }
    public bool? OrderByDesc { get; set; }

    public bool? IsDeleted { get; set; }
}