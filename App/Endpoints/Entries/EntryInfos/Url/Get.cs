using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Url;

public class Get : EndpointBaseAsync
    .WithRequest<EntryInfoGetRequest>
    .WithActionResult<EntryUrl>
{
    private readonly EntryUrlRepository _entryUrlRepository;

    public Get(EntryUrlRepository entryUrlRepository)
    {
        _entryUrlRepository = entryUrlRepository;
    }

    [HttpGet("/api/entries/{entryId}/urls/{entryInfoId}", Name = "EntryUrl_Get")]
    [SwaggerOperation(OperationId = "EntryUrl.Get", Tags = new[] {"EntryUrl"})]
    public override async Task<ActionResult<EntryUrl>> HandleAsync(
        [FromMultiSource] EntryInfoGetRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var eInfo = await _entryUrlRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        return Ok(eInfo);
    }
}