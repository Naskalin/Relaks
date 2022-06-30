using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.StructureConnections;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    .WithActionResult

{
    private readonly StructureConnectionRepository _structureConnectionRepository;

    public List(StructureConnectionRepository structureConnectionRepository)
    {
        _structureConnectionRepository = structureConnectionRepository;
    }

    [HttpGet("/api/structure-connections")]
    [SwaggerOperation(OperationId = "StructureConnection.List", Tags = new[] {"StructureConnection"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] ListRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var items = await _structureConnectionRepository
                .FindByListRequest(request)
                .ToListAsync(cancellationToken)
            ;
        
        return Ok(items);
    }
}