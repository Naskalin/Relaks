using App.Models;
using App.Repository;
using App.Repository.EntryInfoExtends;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos;

public class List : EndpointBaseAsync
    .WithRequest<EntryInfoListRequest>
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
        [FromMultiSource] EntryInfoListRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var eInfos = await _entryInfoRepository.EmailsAsync(request, cancellationToken);
        return Ok(eInfos);
    }
}