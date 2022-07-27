using App.Mappers;

using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.Entries;

[HttpPut("/api/entries/{entryId:guid}"), AllowAnonymous]
public class Put : Endpoint<EntryPutRequest>
{
    private readonly EntryRepository _entryRepository;

    public Put(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public override async Task HandleAsync(EntryPutRequest req, CancellationToken ct)
    {
        var entry = await _entryRepository.FindByIdAsync(req.EntryId, ct);
        if (entry == null) await SendNotFoundAsync(ct);
        else
        {
            req.MapTo(entry);
            await _entryRepository.UpdateAsync(entry, ct);
            await SendNoContentAsync(ct);
        }
    }
}