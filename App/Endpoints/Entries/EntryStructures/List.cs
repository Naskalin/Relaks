using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryStructures;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    .WithActionResult
{
    private readonly StructureRepository _structureRepository;

    public List(StructureRepository structureRepository)
    {
        _structureRepository = structureRepository;
    }

    [HttpGet("/api/entries/{entryId}/structures")]
    [SwaggerOperation(OperationId = "EntryStructure.List", Tags = new[] {"EntryStructure"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] ListRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var tree = await _structureRepository.GetTreeForEntry(request, cancellationToken);
        return Ok(tree);
    }
}