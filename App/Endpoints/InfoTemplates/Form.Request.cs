using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.InfoTemplates;

public class CreateRequest
{
    [FromBody]
    public FormRequestDetails Details { get; set; } = null!;
}

public class PutRequest : CreateRequest
{
    [FromRoute]
    public Guid InfoTemplateId { get; set; }
}

public class FormRequestDetails
{
    public string Title { get; set; } = null!;
    public CustomInfo Template { get; set; } = null!;
}