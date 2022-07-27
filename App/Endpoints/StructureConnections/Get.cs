// using App.Repository;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.StructureConnections;
//
// public class Get : EndpointBaseAsync
//     .WithRequest<Guid>
//     .WithActionResult
// {
//     private readonly StructureConnectionRepository _structureConnectionRepository;
//
//     public Get(StructureConnectionRepository structureConnectionRepository)
//     {
//         _structureConnectionRepository = structureConnectionRepository;
//     }
//
//     [HttpGet("/api/structure-connections/{structureConnectionId}", Name = "StructureConnection_Get")]
//     [SwaggerOperation(OperationId = "StructureConnection.Get", Tags = new[] {"StructureConnection"})]
//     public override async Task<ActionResult> HandleAsync(
//         [FromRoute] Guid structureConnectionId,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var structureConnection = await _structureConnectionRepository
//             .FindByIdAsync(structureConnectionId, cancellationToken);
//         if (structureConnection == null) return NotFound();
//         return Ok(structureConnection);
//     }
// }