using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.Texts;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult
{
    private readonly EntryTextRepository _entryTextRepository;

    public Put(EntryTextRepository entryTextRepository)
    {
        _entryTextRepository = entryTextRepository;
    }

    [HttpPut("/api/entries/{EntryId}/texts/{EntryTextId}")]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] PutRequest request,
        CancellationToken cancellationToken = new())
    {
        var validation = await new CreateRequestValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            return BadRequest(validation);
        }
        
        var entryText = await _entryTextRepository.FindByIdAsync(request.EntryTextId, cancellationToken);
        if (entryText == null || entryText.EntryId != request.EntryId)
        {
            return NotFound();
        }
        
        request.MapTo(entryText);
        entryText.UpdatedAt = DateTime.UtcNow;
        await _entryTextRepository.UpdateAsync(entryText, cancellationToken);

        return NoContent();
    }
}