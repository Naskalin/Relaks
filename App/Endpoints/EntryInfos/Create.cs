using App.Mappers;
using App.Models;
using App.Repository;
using Microsoft.AspNetCore.Authorization;

namespace App.Endpoints.EntryInfos;

[HttpPost("/api/entries/{entryId:guid}/entry-infos"), AllowAnonymous]
public class Create : Endpoint<EntryInfoCreateRequest>
{
    private readonly EntryRepository _entryRepository;
    private readonly EntryInfoRepository _entryInfoRepository;

    public Create(
        EntryRepository entryRepository,
        EntryInfoRepository entryInfoRepository
    )
    {
        _entryRepository = entryRepository;
        _entryInfoRepository = entryInfoRepository;
    }

    public override async Task HandleAsync(EntryInfoCreateRequest req, CancellationToken ct)
    {
        var entry = await _entryRepository.FindByIdAsync(req.EntryId, ct);
        if (entry == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var eInfo = new EntryInfo {
            EntryId = entry.Id, 
            CreatedAt = DateTime.UtcNow,
            Type = req.Type
        };

        req.MapTo(eInfo);
        
        await _entryInfoRepository.CreateAsync(eInfo, ct);
        await SendCreatedAtAsync<Get>(new {entryId = entry.Id, entryInfoId = eInfo.Id}, eInfo, cancellation: ct);
    }
}