// using App.Mappers;
// using App.Models;
// using App.Repository;
// using App.Utils;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Options;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Entries.EntryInfos.Date;
//
// public class Create : EndpointBaseAsync
//     .WithRequest<CreateRequest>
//     .WithActionResult
// {
//     private readonly EntryDateRepository _entryDateRepository;
//     private readonly IOptions<ApiBehaviorOptions> _apiOptions;
//     private readonly EntryRepository _entryRepository;
//
//     public Create(
//         EntryDateRepository entryDateRepository,
//         IOptions<ApiBehaviorOptions> apiOptions,
//         EntryRepository entryRepository
//     )
//     {
//         _entryDateRepository = entryDateRepository;
//         _apiOptions = apiOptions;
//         _entryRepository = entryRepository;
//     }
//
//     [HttpPost("/api/entries/{entryId}/dates")]
//     [SwaggerOperation(OperationId = "EntryDate.Create", Tags = new[] {"EntryDate"})]
//     public override async Task<ActionResult> HandleAsync(
//         [FromMultiSource] CreateRequest request,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var validation = await new CreateRequestValidator().ValidateAsync(request.Details, cancellationToken);
//         if (!validation.IsValid)
//         {
//             validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
//             return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
//         }
//
//         var entry = await _entryRepository.FindByIdAsync(request.EntryId, cancellationToken);
//         if (entry == null)
//         {
//             return NotFound();
//         }
//
//         var eInfo = new EntryDate {EntryId = entry.Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow};
//         request.Details.MapTo(eInfo);
//         await _entryDateRepository.CreateAsync(eInfo, cancellationToken);
//
//         entry.UpdatedAt = DateTime.UtcNow;
//         await _entryRepository.UpdateAsync(entry, cancellationToken);
//
//         return CreatedAtRoute("EntryDate_Get", new {entryId = entry.Id, entryInfoId = eInfo.Id}, eInfo);
//     }
// }