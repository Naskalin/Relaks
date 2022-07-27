using App.Mappers;
using App.Repository;
using App.Models;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.Entries;

[HttpPost("/api/entries"), AllowAnonymous]
public class Create : Endpoint<EntryCreateRequest, Entry>
{
    private readonly EntryRepository _entryRepository;

    public Create(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public override async Task HandleAsync(EntryCreateRequest req, CancellationToken ct)
    {
        var entry = new Entry
        {
            CreatedAt = DateTime.UtcNow,
        };
        req.MapTo(entry);

        await _entryRepository.CreateAsync(entry, ct);
        await SendCreatedAtAsync<Get>(new {entryId = entry.Id}, entry, cancellation: ct);
    }
}