using App.Mappers;
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.InfoTemplates;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult
{
    private readonly InfoTemplateRepository _infoTemplateRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Put(InfoTemplateRepository infoTemplateRepository, IOptions<ApiBehaviorOptions> apiOptions)
    {
        _infoTemplateRepository = infoTemplateRepository;
        _apiOptions = apiOptions;
    }

    [HttpPut("/api/info-templates/{infoTemplateId}")]
    [SwaggerOperation(OperationId = "InfoTemplate.Put", Tags = new[] {"InfoTemplate"})]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] PutRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var validation = await new FormRequestDetailsValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
        
        var infoTemplate = await _infoTemplateRepository.FindByIdAsync(request.InfoTemplateId, cancellationToken);
        if (infoTemplate == null) return NotFound();
        
        request.Details.MapTo(infoTemplate);
        await _infoTemplateRepository.UpdateAsync(infoTemplate, cancellationToken);
        return NoContent();
    }
}