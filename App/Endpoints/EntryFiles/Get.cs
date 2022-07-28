using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.EntryFiles;

[HttpGet("/api/entries/{entryId:guid}/files/{entryFileId:guid}"), AllowAnonymous]
public class Get : Endpoint<EntryFileGetRequest, EntryFile>
{
    private readonly EntryFileRepository _entryFileRepository;

    public Get(EntryFileRepository entryFileRepository)
    {
        _entryFileRepository = entryFileRepository;
    }

    public override async Task HandleAsync(EntryFileGetRequest req, CancellationToken ct)
    {
        var entryFile = await _entryFileRepository.FindByIdAsync(req.EntryFileId, ct);
        if (entryFile == null || entryFile.EntryId != req.EntryId)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(entryFile, ct);
    }
}