using App.Mappers;
using App.Models;
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult<Entry>
{
    private readonly EntryRepository _entryRepository;

    public Create(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    [HttpPost("/api/entries")]
    public override async Task<ActionResult<Entry>> HandleAsync(
        CreateRequest createRequest,
        CancellationToken cancellationToken = new())
    {
        var entry = new Entry()
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        createRequest.MapTo(entry);

        _entryRepository.Create(entry);
        await _entryRepository.SaveChangesAsync();

        return CreatedAtRoute("entries_get", new {id = entry.Id}, entry);
    }
}