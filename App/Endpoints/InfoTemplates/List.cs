using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.InfoTemplates;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    .WithActionResult
{
    private readonly InfoTemplateRepository _infoTemplateRepository;

    public List(InfoTemplateRepository infoTemplateRepository)
    {
        _infoTemplateRepository = infoTemplateRepository;
    }

    [HttpGet("/api/info-templates")]
    [SwaggerOperation(OperationId = "InfoTemplate.List", Tags = new[] {"InfoTemplate"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] ListRequest listRequest,
        CancellationToken cancellationToken = new())
    {
        var templates = await _infoTemplateRepository
            .FindByListRequest(listRequest)
            .ToListAsync(cancellationToken);

        return Ok(templates);
    }
}