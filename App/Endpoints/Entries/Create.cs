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
        [FromBody] CreateRequest createRequest,
        CancellationToken cancellationToken = new())
    {
        var validation = await new CreateRequestValidator().ValidateAsync(createRequest, cancellationToken);
        if (!validation.IsValid)
        {
            return BadRequest(validation);
        }

        var entry = new Entry()
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        createRequest.MapTo(entry);

        await _entryRepository.CreateAsync(entry, cancellationToken);
        return CreatedAtRoute("Entries_Get", new {id = entry.Id}, entry);
    }
}