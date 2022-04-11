using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryFiles;

public class Get : EndpointBaseAsync
    .WithRequest<GetRequest>
    .WithActionResult<EntryFile>
{
    private readonly EntryFileRepository _entryFileRepository;

    public Get(EntryFileRepository entryFileRepository)
    {
        _entryFileRepository = entryFileRepository;
    }

    [HttpGet("/api/entries/{entryId}/files/{entryFileId}")]
    [SwaggerOperation(OperationId = "EntryFile.Get", Tags = new[] {"EntryFile"})]
    public override async Task<ActionResult<EntryFile>> HandleAsync(
        [FromMultiSource] GetRequest req,
        CancellationToken cancellationToken = new()
    )
    {
        var entryFile = await _entryFileRepository.FindByIdAsync(req.EntryFileId, cancellationToken);
        if (entryFile == null || entryFile.EntryId != req.EntryId)
        {
            return NotFound();
        }

        return Ok(entryFile);
    }
}