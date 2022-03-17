namespace App.Endpoints.Base;

public class BaseListRequest
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public string? Search { get; set; }
    public string? OrderBy { get; set; }
    public bool? OrderByDesc { get; set; }
}