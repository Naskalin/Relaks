// using App.Models;
// using App.Repository;
// using App.Utils;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Entries.EntryInfos.Email;
//
// public class Get : EndpointBaseAsync
//     .WithRequest<EntryInfoGetRequest>
//     .WithActionResult<EntryEmail>
// {
//     private readonly EntryEmailRepository _entryEmailRepository;
//
//     public Get(EntryEmailRepository entryEmailRepository)
//     {
//         _entryEmailRepository = entryEmailRepository;
//     }
//
//     [HttpGet("/api/entries/{entryId}/emails/{entryInfoId}", Name = "EntryEmail_Get")]
//     [SwaggerOperation(OperationId = "EntryEmail.Get", Tags = new[] {"EntryEmail"})]
//     public override async Task<ActionResult<EntryEmail>> HandleAsync(
//         [FromMultiSource] EntryInfoGetRequest request,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var eInfo = await _entryEmailRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
//         if (eInfo == null || eInfo.EntryId != request.EntryId)
//         {
//             return NotFound();
//         }
//
//         return Ok(eInfo);
//     }
// }