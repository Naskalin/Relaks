using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryFiles;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    .WithActionResult<List<EntryFile>>
{
    private readonly EntryFileRepository _entryFileRepository;

    public List(EntryFileRepository entryFileRepository)
    {
        _entryFileRepository = entryFileRepository;
    }

    [HttpGet("/api/entries/{entryId}/files")]
    [SwaggerOperation(OperationId = "EntryFile.List", Tags = new[] {"EntryFile"})]
    public override async Task<ActionResult<List<EntryFile>>> HandleAsync(
        [FromMultiSource] ListRequest request,
        CancellationToken cancellationToken = new())
    {
        var files = await _entryFileRepository.PaginateListAsync(request, cancellationToken);
        return Ok(files);
    }
}