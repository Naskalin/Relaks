// using App.Models;
// using App.Repository;
// using App.Utils;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Entries.EntryInfos;
//
// public class List : EndpointBaseAsync
//     .WithRequest<ListRequest>
//     .WithActionResult<List<EntryText>>
// {
//     private readonly EntryTextRepository _entryTextRepository;
//     private readonly EntryRepository _entryRepository;
//
//     public List(EntryTextRepository entryTextRepository, EntryRepository entryRepository)
//     {
//         _entryTextRepository = entryTextRepository;
//         _entryRepository = entryRepository;
//     }
//
//     [HttpGet("/api/entries/{EntryId}/texts")]
//     [SwaggerOperation(OperationId = "EntryText.List", Tags = new[] {"EntryText"})]
//     public override async Task<ActionResult<List<EntryText>>> HandleAsync(
//         [FromMultiSource] ListRequest listRequest, 
//         CancellationToken cancellationToken = new())
//     {
//         var validation = await new ListRequestValidator().ValidateAsync(listRequest, cancellationToken);
//         if (!validation.IsValid)
//         {
//             return BadRequest(validation);
//         } 
//         
//         var entry = await _entryRepository.FindByIdAsync(listRequest.EntryId, cancellationToken);
//         if (entry == null)
//         {
//             return NotFound();
//         }
//         
//         var texts = await _entryTextRepository.PaginateListAsync(listRequest, cancellationToken);
//         return Ok(texts);
//     }
// }