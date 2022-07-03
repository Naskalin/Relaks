using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.InfoTemplates;

public class Delete : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    private readonly InfoTemplateRepository _infoTemplateRepository;

    public Delete(InfoTemplateRepository infoTemplateRepository)
    {
        _infoTemplateRepository = infoTemplateRepository;
    }

    [HttpDelete("/api/info-templates/{infoTemplateId}")]
    [SwaggerOperation(OperationId = "InfoTemplate.Delete", Tags = new[] {"InfoTemplate"})]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] Guid infoTemplateId, 
        CancellationToken cancellationToken = new())
    {
        var infoTemplate = await _infoTemplateRepository.FindByIdAsync(infoTemplateId, cancellationToken);
        if (infoTemplate == null) return NotFound();

        await _infoTemplateRepository.DeleteAsync(infoTemplate, cancellationToken);
        return NoContent();
    }
}