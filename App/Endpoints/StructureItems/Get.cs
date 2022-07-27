// using App.Repository;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.StructureItems;
//
// public class Get : EndpointBaseAsync
//     .WithRequest<Guid>
//     .WithActionResult
// {
//     private readonly StructureItemRepository _structureItemRepository;
//
//     public Get(StructureItemRepository structureItemRepository)
//     {
//         _structureItemRepository = structureItemRepository;
//     }
//
//     [HttpGet("/api/structure-items/{structureItemId}", Name = "StructureItem_Get")]
//     [SwaggerOperation(OperationId = "StructureItem.Get", Tags = new[] {"StructureItem"})]
//     public override async Task<ActionResult> HandleAsync(
//         [FromRoute] Guid structureItemId,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var structureItem = await _structureItemRepository.FindByIdWithRelationsAsync(
//             structureItemId, cancellationToken
//         );
//         if (structureItem == null) return NotFound();
//
//         return Ok(structureItem);
//     }
// }