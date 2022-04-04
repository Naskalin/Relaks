using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Note;

public class Get : EndpointBaseAsync
    .WithRequest<EInfoGetRequest>
    .WithActionResult<EntryNote>
{
    private readonly EntryNoteRepository _entryNoteRepository;

    public Get(EntryNoteRepository entryNoteRepository)
    {
        _entryNoteRepository = entryNoteRepository;
    }

    [HttpGet("/api/entries/{entryId}/notes/{entryInfoId}", Name = "Entries_Notes_Get")]
    [SwaggerOperation(OperationId = "EntryNote.Get", Tags = new[] {"EntryNote"})]
    public override async Task<ActionResult<EntryNote>> HandleAsync(
        [FromMultiSource] EInfoGetRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var eInfo = await _entryNoteRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        return Ok(eInfo);
    }
}