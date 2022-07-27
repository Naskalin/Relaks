using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.Entries;

[HttpGet("/api/entries/{entryId:guid}"), AllowAnonymous]
public class Get : Endpoint<EntryGetRequest, Entry>
{
    private readonly EntryRepository _entryRepository;

    public Get(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }
    
    public override async Task HandleAsync(EntryGetRequest req, CancellationToken ct)
    {
        var entry = await _entryRepository.FindByIdAsync(req.EntryId, ct);
        if (entry == null) await SendNotFoundAsync(ct);
        else await SendAsync(entry, cancellation: ct);
    }
}