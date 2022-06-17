using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryStructures;

public class Delete : EndpointBaseAsync
    .WithRequest<GetRequest>
    .WithActionResult
{
    private readonly StructureRepository _structureRepository;

    public Delete(StructureRepository structureRepository)
    {
        _structureRepository = structureRepository;
    }

    [HttpDelete("/api/entries/{entryId}/structures/{structureId}")]
    [SwaggerOperation(OperationId = "EntryStructure.Delete", Tags = new[] {"EntryStructure"})]
    public override async Task<ActionResult> HandleAsync(
        GetRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var structure = await _structureRepository.FindByIdAsync(request.StructureId, cancellationToken);
        if (structure == null || structure.EntryId != request.EntryId) return NotFound();

        await _structureRepository.DeleteAsync(structure, cancellationToken);

        return NoContent();
    }
}