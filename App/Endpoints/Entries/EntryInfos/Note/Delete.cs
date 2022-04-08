using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Note;

public class Delete : EndpointBaseAsync
    .WithRequest<EntryInfoDeleteRequest>
    .WithActionResult
{
    private readonly EntryNoteRepository _entryNoteRepository;

    public Delete(EntryNoteRepository entryNoteRepository)
    {
        _entryNoteRepository = entryNoteRepository;
    }

    [HttpDelete("/api/entries/{entryId}/notes/{entryInfoId}")]
    [SwaggerOperation(OperationId = "EntryNote.Delete", Tags = new[] {"EntryNote"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] EntryInfoDeleteRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var eInfo = await _entryNoteRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        if (request.IsFullDelete == true)
        {
            await _entryNoteRepository.DeleteAsync(eInfo, cancellationToken);
        }
        else
        {
            request.MapTo(eInfo);
            await _entryNoteRepository.UpdateAsync(eInfo, cancellationToken);   
        }
        return NoContent();
    }
}