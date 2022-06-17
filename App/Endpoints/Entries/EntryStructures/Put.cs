using App.Mappers;
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryStructures;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult
{
    private readonly StructureRepository _structureRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;
    
    public Put(StructureRepository structureRepository, IOptions<ApiBehaviorOptions> apiOptions)
    {
        _structureRepository = structureRepository;
        _apiOptions = apiOptions;
    }

    [HttpPut("/api/entries/{entryId}/structures/{structureId}")]
    [SwaggerOperation(OperationId = "EntryStructure.Put", Tags = new[] {"EntryStructure"})]
    public override async Task<ActionResult> HandleAsync(PutRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var validation = await new DetailsValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
        
        var structure = await _structureRepository.FindByIdAsync(request.StructureId, cancellationToken);
        if (structure == null || structure.EntryId != request.EntryId) return NotFound();
        request.Details.MapTo(structure);
        await _structureRepository.UpdateAsync(structure, cancellationToken);

        return NoContent();
    }
}