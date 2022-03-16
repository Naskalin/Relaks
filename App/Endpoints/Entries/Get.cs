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

    [HttpGet("/api/entries/{id}", Name = "entries_get")]
    public override async Task<ActionResult<Entry>> HandleAsync(Guid id, CancellationToken cancellationToken = new())
    {
        var entry = await _entryRepository.FindSingleAsync(id);

        if (entry == null) return NotFound();

        return Ok(entry);
    }
}