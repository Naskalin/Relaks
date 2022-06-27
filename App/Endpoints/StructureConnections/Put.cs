using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.StructureConnections;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult
{
    private readonly StructureConnectionRepository _structureConnectionRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Put(StructureConnectionRepository structureConnectionRepository, IOptions<ApiBehaviorOptions> apiOptions)
    {
        _structureConnectionRepository = structureConnectionRepository;
        _apiOptions = apiOptions;
    }

    [HttpPut("/api/structure-connections/{structureConnectionId}")]
    [SwaggerOperation(OperationId = "StructureConnection.Put", Tags = new[] {"StructureConnection"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] PutRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var validation = await new DetailsValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        var sConnection = await _structureConnectionRepository
            .FindByIdAsync(request.StructureConnectionId, cancellationToken);
        if (sConnection == null) return NotFound();

        request.Details.MapTo(sConnection);
        await _structureConnectionRepository.UpdateAsync(sConnection, cancellationToken);
        return NoContent();
    }
}