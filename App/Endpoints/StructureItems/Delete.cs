// using App.Repository;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.StructureItems;
//
// public class Delete : EndpointBaseAsync
//     .WithRequest<Guid>
//     .WithActionResult
// {
//     private readonly StructureItemRepository _structureItemRepository;
//
//     public Delete(StructureItemRepository structureItemRepository)
//     {
//         _structureItemRepository = structureItemRepository;
//     }
//
//     [HttpDelete("/api/structure-items/{structureItemId}")]
//     [SwaggerOperation(OperationId = "StructureItem.Delete", Tags = new[] {"StructureItem"})]
//     public override async Task<ActionResult> HandleAsync(
//         [FromRoute] Guid structureItemId,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var structureItem = await _structureItemRepository.FindByIdAsync(structureItemId, cancellationToken);
//         if (structureItem == null) return NotFound();
//         await _structureItemRepository.DeleteAsync(structureItem, cancellationToken);
//         return NoContent();
//     }
// }