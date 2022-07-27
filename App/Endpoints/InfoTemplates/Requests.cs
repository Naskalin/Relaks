using App.Models;

namespace App.Endpoints.InfoTemplates;

public class InfoTemplateFormRequest
{
    public string Title { get; set; } = null!;
    public CustomInfo Template { get; set; } = null!;
}

public class CreateRequest : InfoTemplateFormRequest
{
}

public class PutRequest : InfoTemplateFormRequest
{
    public Guid InfoTemplateId { get; set; }
}

public class GetRequest
{
    public Guid InfoTemplateId { get; set; }
}

public class ListRequest : IPaginableRequest, ISearchableRequest
{
    public int? Page { get; set; }
    public int? PerPage { get; set; }

    public string? Search { get; set; }
}