namespace App.Endpoints.Base;

public class BaseRequest
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public string? Search { get; set; }
    public string? OrderBy { get; set; }
    public string? OrderDirection { get; set; }
}