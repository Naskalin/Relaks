using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

    [HttpDelete("/api/entries/{entryId}")]
    [SwaggerOperation(OperationId = "Entry.Delete", Tags = new[] {"Entry"})]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] Guid entryId,
        CancellationToken cancellationToken = new())
    {
        var entry = await _entryRepository.FindByIdAsync(entryId, cancellationToken);
        if (entry == null)
        {
            return NotFound();
        }

        await _entryRepository.TrySoftDelete(entry, cancellationToken);

        return NoContent();
    }
}