using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Structures;

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
    [SwaggerOperation(OperationId = "Structure.Delete", Tags = new[] {"Structure"})]
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