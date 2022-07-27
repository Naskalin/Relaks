using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.Entries;

[HttpGet("/api/entries"), AllowAnonymous]
public class List : Endpoint<EntryListRequest, List<EntryDto>>
{
    private readonly EntryRepository _entryRepository;

    public List(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }
    
    public override async Task HandleAsync(EntryListRequest req, CancellationToken ct)
    {
        var query = _entryRepository.FindByListRequest(req);
        await SendAsync(query.ToList(), cancellation: ct);
    }
}