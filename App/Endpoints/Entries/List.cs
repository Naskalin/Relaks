using App.Models;
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    .WithActionResult<List<Entry>>
{
    private readonly EntryRepository _entryRepository;

    public List(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    [HttpGet("/api/entries")]
    public override async Task<ActionResult<List<Entry>>> HandleAsync(
        [FromQuery] ListRequest listRequest,
        CancellationToken cancellationToken = new())
    {
        var entries = await _entryRepository.FindByRequestAsync(listRequest);
        return Ok(entries);
    }
}