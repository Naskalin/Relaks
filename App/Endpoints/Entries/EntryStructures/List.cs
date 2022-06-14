using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        var structures = await _structureRepository
            .Entities
            .Where(x => x.EntryId.Equals(request.EntryId))
            .Include(x => x.Items)
            .ToListAsync(cancellationToken);
        
        
        return Ok(structures);
    }

    // private List<Structure> ToTree(List<Structure> tree, List<Structure> structures, Guid? parentId)
    // {
    //     var tree = new List<Structure>();
    //
    //     return tree;
    // }
}