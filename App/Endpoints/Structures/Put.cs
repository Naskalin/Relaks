// using App.Mappers;
// using App.Repository;
// using App.Utils;
// using Ardalis.ApiEndpoints;
// using FluentValidation.Results;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Options;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Structures;
//
// public class Put : EndpointBaseAsync
//     .WithRequest<PutRequest>
//     .WithActionResult
// {
//     private readonly StructureRepository _structureRepository;
//     private readonly IOptions<ApiBehaviorOptions> _apiOptions;
//
//     public Put(StructureRepository structureRepository, IOptions<ApiBehaviorOptions> apiOptions)
//     {
//         _structureRepository = structureRepository;
//         _apiOptions = apiOptions;
//     }
//
//     [HttpPut("/api/entries/{entryId}/structures/{structureId}")]
//     [SwaggerOperation(OperationId = "Structure.Put", Tags = new[] {"Structure"})]
//     public override async Task<ActionResult> HandleAsync(
//         [FromMultiSource] PutRequest request,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var validation = await new DetailsValidator().ValidateAsync(request.Details, cancellationToken);
//         if (request.StructureId.Equals(request.Details.ParentId))
//             validation
//                 .Errors
//                 .Add(new ValidationFailure("StructureId", "Группа не может быть родительской для самой себя."));
//         if (!validation.IsValid)
//         {
//             validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
//             return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
//         }
//
//         var structure = await _structureRepository.FindByIdAsync(request.StructureId, cancellationToken);
//         if (structure == null || structure.EntryId != request.EntryId) return NotFound();
//         request.Details.MapTo(structure);
//         await _structureRepository.UpdateAsync(structure, cancellationToken);
//
//         return NoContent();
//     }
// }