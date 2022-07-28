using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.EntryFiles.Meta;

[HttpGet("/api/entries/{entryId:guid}/files/meta"), AllowAnonymous]
public class Get : Endpoint<GetMetaRequest, GetMetaResult>
{
    private readonly EntryFileRepository _entryFileRepository;

    public Get(EntryFileRepository entryFileRepository)
    {
        _entryFileRepository = entryFileRepository;
    }

    public override async Task HandleAsync(GetMetaRequest req, CancellationToken ct)
    {
        var result = await _entryFileRepository.GetEntryMetaAsync(req.EntryId, ct);
        await SendOkAsync(result, ct);
    }
}

public class GetMetaRequest
{
    public Guid EntryId { get; set; }
}

public class GetMetaResult
{
    public List<string> Categories { get; set; } = new();
    public List<string> Tags { get; set; } = new();
}