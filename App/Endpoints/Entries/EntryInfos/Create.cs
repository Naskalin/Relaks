// using App.Mappers;
// using App.Models;
// using App.Repository;
// using App.Utils;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Options;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Entries.EntryInfos;
//
// public class Create : EndpointBaseAsync
//     .WithRequest<CreateRequest>
//     .WithActionResult
// {
//     private readonly EntryRepository _entryRepository;
//     private readonly IOptions<ApiBehaviorOptions> _apiOptions;
//     private readonly BaseRepository<EntryInfoEmail> _infoEmailRepository;
//
//     public Create(
//         EntryRepository entryRepository,
//         IOptions<ApiBehaviorOptions> apiOptions, BaseRepository<EntryInfoEmail> infoEmailRepository)
//     {
//         _entryRepository = entryRepository;
//         _apiOptions = apiOptions;
//         _infoEmailRepository = infoEmailRepository;
//     }
//
//     [HttpPost("/api/entries/{EntryId}/infos")]
//     [SwaggerOperation(OperationId = "EntryText.Create", Tags = new[] {"EntryText"})]
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
//         switch (request.Details.Discriminator)
//         {
//             case EntryInfo.EmailType:
//                 var email = new EntryInfoEmail
//                     {EntryId = entry.Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow};
//                 request.Details.MapTo(email);
//                 break;
//             case EntryInfo.PhoneType:
//                 var infoPhone = new EntryInfoPhone
//                     {EntryId = entry.Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow};
//                 request.Details.MapTo(infoPhone);
//                 break;
//             case EntryInfo.UrlType:
//                 var infoUrl = new EntryInfoUrl
//                     {EntryId = entry.Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow};
//                 request.Details.MapTo(infoUrl);
//                 break;
//             case EntryInfo.NoteType:
//                 var infoNote = new EntryInfoNote
//                     {EntryId = entry.Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow};
//                 request.Details.MapTo(infoNote);
//                 break;
//             case EntryInfo.DateType:
//                 var infoDate = new EntryInfoDate
//                     {EntryId = entry.Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow};
//                 request.Details.MapTo(infoDate);
//                 break;
//         }
//
//         return BadRequest();
//         // var entryText = new EntryText()
//         // {
//         //     EntryId = entry.Id,
//         //     CreatedAt = DateTime.UtcNow,
//         //     UpdatedAt = DateTime.UtcNow,
//         // };
//         // request.MapTo(entryText);
//
//         await _entryTextRepository.CreateAsync(entryText, cancellationToken);
//
//         entry.UpdatedAt = DateTime.UtcNow;
//         await _entryRepository.UpdateAsync(entry, cancellationToken);
//         return CreatedAtRoute("Entries_Texts_Get", new {EntryId = entryText.EntryId, EntryTextId = entryText.Id},
//             entryText);
//     }
// }