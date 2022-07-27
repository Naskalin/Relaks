using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.EntryInfos;

[HttpDelete("/api/entries/{entryId:guid}/entry-infos/{entryInfoId:guid}"), AllowAnonymous]
public class Delete : Endpoint<EntryInfoGetRequest>
{
    private readonly EntryInfoRepository _entryInfoRepository;

    public Delete(EntryInfoRepository entryInfoRepository)
    {
        _entryInfoRepository = entryInfoRepository;
    }
    
    public override async Task HandleAsync(EntryInfoGetRequest req, CancellationToken ct)
    {
        var eInfo = await _entryInfoRepository.FindByIdAsync(req.EntryInfoId, ct);
        if (eInfo == null || eInfo.EntryId != req.EntryId)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        await _entryInfoRepository.DeleteAsync(eInfo, ct);
        await SendNoContentAsync(ct);
    }
}