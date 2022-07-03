using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.InfoTemplates;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult
{
    private readonly InfoTemplateRepository _infoTemplateRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Create(InfoTemplateRepository infoTemplateRepository, IOptions<ApiBehaviorOptions> apiOptions)
    {
        _infoTemplateRepository = infoTemplateRepository;
        _apiOptions = apiOptions;
    }

    [HttpPost("/api/info-templates")]
    [SwaggerOperation(OperationId = "InfoTemplate.Create", Tags = new[] {"InfoTemplate"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] CreateRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var validation = await new FormRequestDetailsValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        var infoTemplate = new InfoTemplate {CreatedAt = DateTime.UtcNow};
        request.Details.MapTo(infoTemplate);
        await _infoTemplateRepository.CreateAsync(infoTemplate, cancellationToken);
        return CreatedAtRoute("InfoTemplate_Get", new {infoTemplateId = infoTemplate.Id}, infoTemplate);
    }
}