using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.InfoTemplates;

public class ListRequest : IPaginableRequest, ISearchableRequest
{
    [FromQuery]
    public int? Page { get; set; }

    [FromQuery]
    public int? PerPage { get; set; }

    [FromQuery]
    public string? Search { get; set; }
}