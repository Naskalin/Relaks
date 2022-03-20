using App.Models;
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries;

public class Get : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<Entry>
{
    private readonly EntryRepository _entryRepository;

    public Get(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    [HttpGet("/api/entries/{entryId}", Name = "Entries_Get")]
    [SwaggerOperation(OperationId = "Entry.Get", Tags = new[] {"Entry"})]
    public override async Task<ActionResult<Entry>> HandleAsync(
        [FromRoute] Guid entryId, 
        CancellationToken cancellationToken = new())
    {
        var entry = await _entryRepository.FindByIdAsync(entryId, cancellationToken);

        if (entry == null) return NotFound();

        return Ok(entry);
    }
}