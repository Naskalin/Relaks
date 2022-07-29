using App.Mappers;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.EntryInfos;

[HttpPut("/api/entries/{entryId:guid}/entry-infos/{entryInfoId:guid}"), AllowAnonymous]
public class Put : Endpoint<EntryInfoPutRequest>
{
    private readonly EntryInfoRepository _entryInfoRepository;

    public Put(EntryInfoRepository entryInfoRepository)
    {
        _entryInfoRepository = entryInfoRepository;
    }

    public override async Task HandleAsync(EntryInfoPutRequest req, CancellationToken ct)
    {
        var eInfo = await _entryInfoRepository.FindByIdAsync(req.EntryInfoId, ct);
        if (eInfo == null || eInfo.EntryId != req.EntryId)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        req.MapTo(eInfo);
        await _entryInfoRepository.UpdateAsync(eInfo, ct);
        await SendNoContentAsync(ct);
    }
}