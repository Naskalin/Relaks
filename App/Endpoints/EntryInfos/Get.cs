using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.EntryInfos;

[HttpGet("/api/entries/{entryId:guid}/entry-infos/{entryInfoId:guid}"), AllowAnonymous]
public class Get : Endpoint<EntryInfoGetRequest, EntryInfo>
{
    private readonly EntryInfoRepository _entryInfoRepository;

    public Get(EntryInfoRepository entryInfoRepository)
    {
        _entryInfoRepository = entryInfoRepository;
    }
    
    public override async Task HandleAsync(EntryInfoGetRequest request, CancellationToken ct)
    {
        var eInfo = await _entryInfoRepository.FindByIdAsync(request.EntryInfoId, ct);
        if (eInfo == null || eInfo.EntryId != request.EntryId) await SendNotFoundAsync(ct);
        else await SendAsync(eInfo, cancellation: ct);
    }
}