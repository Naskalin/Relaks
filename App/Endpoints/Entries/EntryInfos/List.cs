using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos;

public class List : EndpointBaseAsync
    .WithRequest<EntryInfoListDeletableRequest>
    .WithActionResult
{
    private readonly EntryInfoRepository _entryInfoRepository;

    public List(EntryInfoRepository entryInfoRepository)
    {
        _entryInfoRepository = entryInfoRepository;
    }

    [HttpGet("/api/entries/{entryId}/entry-infos")]
    [SwaggerOperation(OperationId = "EntryInfo.List", Tags = new[] {"EntryInfo"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] EntryInfoListDeletableRequest deletableRequest,
        CancellationToken cancellationToken = new()
    )
    {
        var eInfos = await _entryInfoRepository.PaginateAsync(deletableRequest, cancellationToken);
        return Ok(eInfos);
    }
}