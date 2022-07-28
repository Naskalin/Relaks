using App.Mappers;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.EntryFiles;

[HttpPut("/api/entries/{entryId:guid}/files/{entryFileId:guid}"), AllowAnonymous]
public class Put : Endpoint<EntryFilePutRequest>
{
    private readonly EntryFileRepository _entryFileRepository;

    public Put(EntryFileRepository entryFileRepository)
    {
        _entryFileRepository = entryFileRepository;
    }

    public override async Task HandleAsync(EntryFilePutRequest req, CancellationToken ct)
    {
        var entryFile = await _entryFileRepository.FindByIdAsync(req.EntryFileId, ct);
        if (entryFile == null || entryFile.EntryId != req.EntryId)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        req.MapTo(entryFile);
        await _entryFileRepository.UpdateAsync(entryFile, ct);

        await SendNoContentAsync(ct);
    }
}