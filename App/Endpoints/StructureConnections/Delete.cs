// using App.Repository;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.StructureConnections;
//
// public class Delete : EndpointBaseAsync
//     .WithRequest<Guid>
//     .WithActionResult
// {
//     private readonly StructureConnectionRepository _structureConnectionRepository;
//
//     public Delete(StructureConnectionRepository structureConnectionRepository)
//     {
//         _structureConnectionRepository = structureConnectionRepository;
//     }
//
//     [HttpDelete("/api/structure-connections/{structureConnectionId}")]
//     [SwaggerOperation(OperationId = "StructureConnection.Delete", Tags = new[] {"StructureConnection"})]
//     public override async Task<ActionResult> HandleAsync(
//         [FromRoute] Guid structureConnectionId,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var structureConnection = await _structureConnectionRepository
//             .FindByIdAsync(structureConnectionId, cancellationToken);
//         if (structureConnection == null) return NotFound();
//         await _structureConnectionRepository.DeleteAsync(structureConnection, cancellationToken);
//         return NoContent();
//     }
// }