using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.StructureItems;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    .WithActionResult
{
    private readonly StructureItemRepository _structureItemRepository;

    public List(StructureItemRepository structureItemRepository)
    {
        _structureItemRepository = structureItemRepository;
    }

    [HttpGet("/api/structure-items")]
    [SwaggerOperation(OperationId = "StructureItem.List", Tags = new[] {"StructureItem"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] ListRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var structureItems = await _structureItemRepository.FindByListRequestAsync(request, cancellationToken);
        return Ok(structureItems);
    }
}