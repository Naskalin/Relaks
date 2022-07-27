using App.Repository;

using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.EntryInfos;

[HttpGet("/api/entries/{entryId}/entry-infos"), AllowAnonymous]
public class List : Endpoint<EntryInfoListRequest>
{
    private readonly EntryInfoRepository _entryInfoRepository;

    public List(EntryInfoRepository entryInfoRepository)
    {
        _entryInfoRepository = entryInfoRepository;
    }

    public override async Task HandleAsync(EntryInfoListRequest req, CancellationToken ct)
    {
        var eInfos = await _entryInfoRepository.PaginateAsync(req, ct);
        await SendAsync(eInfos, cancellation: ct);
    }
}