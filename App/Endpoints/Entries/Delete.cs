using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.Entries;

[HttpDelete("/api/entries/{entryId:guid}"), AllowAnonymous]
public class Delete : Endpoint<EntryGetRequest>
{
    private readonly EntryRepository _entryRepository;

    public Delete(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public override async Task HandleAsync(EntryGetRequest req, CancellationToken ct)
    {
        var entry = await _entryRepository.FindByIdAsync(req.EntryId, ct);
        if (entry == null) await SendNotFoundAsync(ct);
        else
        {
            await _entryRepository.DeleteAsync(entry, ct);
            await SendNoContentAsync(ct);
        }
    }
}