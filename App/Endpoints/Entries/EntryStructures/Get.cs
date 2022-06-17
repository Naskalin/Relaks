using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryStructures;

public class Get : EndpointBaseAsync
    .WithRequest<GetRequest>
    .WithActionResult
{
    private readonly StructureRepository _structureRepository;

    public Get(StructureRepository structureRepository)
    {
        _structureRepository = structureRepository;
    }

    [HttpGet("/api/entries/{entryId}/structures/{structureId}", Name = "EntryStructure_Get")]
    [SwaggerOperation(OperationId = "EntryStructure.Get", Tags = new[] {"EntryStructure"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] GetRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var structure = await _structureRepository.FindByIdAsync(request.StructureId, cancellationToken);
        if (structure == null || structure.EntryId != request.EntryId) return NotFound();

        return Ok(structure);
    }
}