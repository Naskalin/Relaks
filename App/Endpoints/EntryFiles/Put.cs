// using App.Mappers;
// using App.Repository;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Options;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Entries.EntryFiles;
//
// public class Put : EndpointBaseAsync
//     .WithRequest<PutRequest>
//     .WithActionResult
// {
//     private readonly EntryFileRepository _entryFileRepository;
//     private readonly IOptions<ApiBehaviorOptions> _apiOptions;
//
//     public Put(EntryFileRepository entryFileRepository, IOptions<ApiBehaviorOptions> apiOptions)
//     {
//         _entryFileRepository = entryFileRepository;
//         _apiOptions = apiOptions;
//     }
//
//     [HttpPut("/api/entries/{entryId}/files/{entryFileId}")]
//     [SwaggerOperation(OperationId = "EntryFile.Put", Tags = new[] {"EntryFile"})]
//     public override async Task<ActionResult> HandleAsync(
//         PutRequest req,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var validation = await new PutRequestValidator().ValidateAsync(req.Details, cancellationToken);
//         if (!validation.IsValid)
//         {
//             validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
//             return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
//         }
//         
//         var entryFile = await _entryFileRepository.FindByIdAsync(req.EntryFileId, cancellationToken);
//         if (entryFile == null || entryFile.EntryId != req.EntryId)
//         {
//             return NotFound();
//         }
//         
//         req.Details.MapTo(entryFile);
//         await _entryFileRepository.UpdateAsync(entryFile, cancellationToken);
//
//         return NoContent();
//     }
// }