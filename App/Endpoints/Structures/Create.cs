using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Structures;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult
{
    private readonly StructureRepository _structureRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Create(StructureRepository structureRepository, IOptions<ApiBehaviorOptions> apiOptions)
    {
        _structureRepository = structureRepository;
        _apiOptions = apiOptions;
    }

    [HttpPost("/api/entries/{entryId}/structures")]
    [SwaggerOperation(OperationId = "Structure.Create", Tags = new[] {"Structure"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] CreateRequest request, 
        CancellationToken cancellationToken = new())
    {
        var validation = await new DetailsValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
        
        var structure = new Structure()
        {
            EntryId = request.EntryId,
            CreatedAt = DateTime.UtcNow
        };
        request.Details.MapTo(structure);
        await _structureRepository.CreateAsync(structure, cancellationToken);

        return CreatedAtRoute("Structure_Get", new {entryId = request.EntryId, structureId = structure.Id}, structure);
    }
}