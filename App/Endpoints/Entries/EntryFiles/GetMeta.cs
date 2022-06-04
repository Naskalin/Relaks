using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryFiles;

public class GetMeta : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<GetMetaResult>
{
    private readonly EntryFileRepository _entryFileRepository;

    public GetMeta(EntryFileRepository entryFileRepository)
    {
        _entryFileRepository = entryFileRepository;
    }

    [HttpGet("/api/entries/{entryId}/files/meta")]
    [SwaggerOperation(OperationId = "EntryFile.GetMeta", Tags = new[] {"EntryFile"})]
    public override async Task<ActionResult<GetMetaResult>> HandleAsync(
        [FromRoute] Guid entryId,
        CancellationToken cancellationToken = new()
    )
    {
        var result = await _entryFileRepository.GetEntryMetaAsync(entryId, cancellationToken);
        return Ok(result);
    }
}