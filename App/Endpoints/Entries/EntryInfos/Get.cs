using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos;

public class Get : EndpointBaseAsync
    .WithRequest<EntryInfoGetRequest>
    .WithActionResult<EntryInfo>
{
    private readonly EntryInfoRepository _entryInfoRepository;

    public Get(EntryInfoRepository entryInfoRepository)
    {
        _entryInfoRepository = entryInfoRepository;
    }

    [HttpGet("/api/entries/{entryId}/entry-infos/{entryInfoId}", Name = "EntryInfo_Get")]
    [SwaggerOperation(OperationId = "EntryInfo.Get", Tags = new[] {"EntryInfo"})]
    public override async Task<ActionResult<EntryInfo>> HandleAsync(
        [FromMultiSource] EntryInfoGetRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var eInfo = await _entryInfoRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        return Ok(eInfo);
    }
}