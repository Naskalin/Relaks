// using App.Models;
// using App.Repository;
// using App.Utils;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Entries.EntryInfos.Phone;
//
// public class List : EndpointBaseAsync
//     .WithRequest<EntryInfoListRequest>
//     .WithActionResult<List<EntryPhone>>
// {
//     private readonly EntryPhoneRepository _entryPhoneRepository;
//
//     public List(EntryPhoneRepository entryPhoneRepository)
//     {
//         _entryPhoneRepository = entryPhoneRepository;
//     }
//
//     [HttpGet("/api/entries/{entryId}/phones")]
//     [SwaggerOperation(OperationId = "EntryPhone.List", Tags = new[] {"EntryPhone"})]
//     public override async Task<ActionResult<List<EntryPhone>>> HandleAsync(
//         [FromMultiSource] EntryInfoListRequest request,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var eInfos = await _entryPhoneRepository.PaginateListAsync(request, cancellationToken);
//         return Ok(eInfos);
//     }
// }