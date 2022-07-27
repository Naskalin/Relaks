// using App.Repository;
// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace App.Endpoints.Entries.EntryFiles.Meta;
//
// public class Get : EndpointBaseAsync
//     .WithRequest<Guid>
//     .WithActionResult<GetMetaResult>
// {
//     private readonly EntryFileRepository _entryFileRepository;
//
//     public Get(EntryFileRepository entryFileRepository)
//     {
//         _entryFileRepository = entryFileRepository;
//     }
//
//     [HttpGet("/api/entries/{entryId}/files/meta")]
//     [SwaggerOperation(OperationId = "EntryFileMeta.Get", Tags = new[] {"EntryFileMeta"})]
//     public override async Task<ActionResult<GetMetaResult>> HandleAsync(
//         [FromRoute] Guid entryId,
//         CancellationToken cancellationToken = new()
//     )
//     {
//         var result = await _entryFileRepository.GetEntryMetaAsync(entryId, cancellationToken);
//         return Ok(result);
//     }
// }