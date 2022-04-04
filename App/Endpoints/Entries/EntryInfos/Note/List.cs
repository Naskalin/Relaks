using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Note;

public class List : EndpointBaseAsync
    .WithRequest<EInfoListRequest>
    .WithActionResult<List<EntryNote>>
{
    private readonly EntryNoteRepository _entryNoteRepository;

    public List(EntryNoteRepository entryNoteRepository)
    {
        _entryNoteRepository = entryNoteRepository;
    }

    [HttpGet("/api/entries/{entryId}/notes")]
    [SwaggerOperation(OperationId = "EntryNote.List", Tags = new[] {"EntryNote"})]
    public override async Task<ActionResult<List<EntryNote>>> HandleAsync(
        [FromMultiSource] EInfoListRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var eInfos = await _entryNoteRepository.PaginateListAsync(request, cancellationToken);
        return Ok(eInfos);
    }
}