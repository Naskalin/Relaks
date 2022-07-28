using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.EntryFiles;

[HttpGet("/api/entries/{entryId:guid}/files"), AllowAnonymous]
public class List : Endpoint<EntryFileListRequest>
{
    private readonly EntryFileRepository _entryFileRepository;

    public List(EntryFileRepository entryFileRepository)
    {
        _entryFileRepository = entryFileRepository;
    }

    public override async Task HandleAsync(EntryFileListRequest req, CancellationToken ct)
    {
        var files = await _entryFileRepository.PaginateListAsync(req, ct);
        await SendOkAsync(files, ct);
    }
}