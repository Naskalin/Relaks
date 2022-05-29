// using App.Mappers;
// using App.Repository;
// using App.Utils;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Options;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Entries.EntryInfos.Phone;
//
// public class Put : EndpointBaseAsync
//     .WithRequest<PutRequest>
//     .WithActionResult
// {
//     private readonly EntryPhoneRepository _entryPhoneRepository;
//     private readonly IOptions<ApiBehaviorOptions> _apiOptions;
//
//     public Put(EntryPhoneRepository entryPhoneRepository, IOptions<ApiBehaviorOptions> apiOptions)
//     {
//         _entryPhoneRepository = entryPhoneRepository;
//         _apiOptions = apiOptions;
//     }
//
//     [HttpPut("/api/entries/{entryId}/phones/{entryInfoId}")]
//     [SwaggerOperation(OperationId = "EntryPhone.Put", Tags = new[] {"EntryPhone"})]
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
//         var eInfo = await _entryPhoneRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
//         if (eInfo == null || eInfo.EntryId != request.EntryId)
//         {
//             return NotFound();
//         }
//         
//         request.Details.MapTo(eInfo);
//         eInfo.UpdatedAt = DateTime.UtcNow;
//         await _entryPhoneRepository.UpdateAsync(eInfo, cancellationToken);
//
//         return NoContent();
//     }
// }