// using App.Mappers;
// using App.Repository;
// using App.Utils;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Options;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Entries.EntryInfos.Email;
//
// public class Put : EndpointBaseAsync
//     .WithRequest<PutRequest>
//     .WithActionResult
// {
//     private readonly EntryEmailRepository _entryEmailRepository;
//     private readonly IOptions<ApiBehaviorOptions> _apiOptions;
//
//     public Put(EntryEmailRepository entryEmailRepository, IOptions<ApiBehaviorOptions> apiOptions)
//     {
//         _entryEmailRepository = entryEmailRepository;
//         _apiOptions = apiOptions;
//     }
//
//     [HttpPut("/api/entries/{entryId}/emails/{entryInfoId}")]
//     [SwaggerOperation(OperationId = "EntryEmail.Put", Tags = new[] {"EntryEmail"})]
//     public override async Task<ActionResult> HandleAsync(
//         [FromMultiSource] PutRequest request,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var validation = await new FormRequestValidator().ValidateAsync(request.Details, cancellationToken);
//         if (!validation.IsValid)
//         {
//             validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
//             return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
//         }
//
//         var eInfo = await _entryEmailRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
//         if (eInfo == null || eInfo.EntryId != request.EntryId)
//         {
//             return NotFound();
//         }
//
//         request.Details.MapTo(eInfo);
//         eInfo.UpdatedAt = DateTime.UtcNow;
//         await _entryEmailRepository.UpdateAsync(eInfo, cancellationToken);
//
//         return NoContent();
//     }
// }