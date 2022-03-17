using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries;

public class Delete : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    private readonly EntryRepository _entryRepository;

    public Delete(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    [HttpDelete("/api/entries/{id}")]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = new())
    {
        var entry = await _entryRepository.FindByIdAsync(id, cancellationToken);
        if (entry == null)
        {
            return NotFound();
        }

        await _entryRepository.DeleteAsync(entry, cancellationToken);

        return NoContent();
    }
}