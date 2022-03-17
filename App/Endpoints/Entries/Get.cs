using App.Models;
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("/api/entries/{id}", Name = "Entries_Get")]
    public override async Task<ActionResult<Entry>> HandleAsync(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken = new())
    {
        var entry = await _entryRepository.FindByIdAsync(id, cancellationToken);

        if (entry == null) return NotFound();

        return Ok(entry);
    }
}