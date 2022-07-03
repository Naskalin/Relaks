using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.InfoTemplates;

public class Get : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    private readonly InfoTemplateRepository _infoTemplateRepository;

    public Get(InfoTemplateRepository infoTemplateRepository)
    {
        _infoTemplateRepository = infoTemplateRepository;
    }

    [HttpGet("/api/info-templates/{infoTemplateId}", Name = "InfoTemplate_Get")]
    [SwaggerOperation(OperationId = "InfoTemplate.Get", Tags = new[] {"InfoTemplate"})]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] Guid infoTemplateId,
        CancellationToken cancellationToken = new()
    )
    {
        var infoTemplate = await _infoTemplateRepository.FindByIdAsync(infoTemplateId, cancellationToken);
        if (infoTemplate == null) return NotFound();
        return Ok(infoTemplate);
    }
}