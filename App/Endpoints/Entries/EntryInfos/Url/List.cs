using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Url;

public class List : EndpointBaseAsync
    .WithRequest<EntryInfoListRequest>
    .WithActionResult<List<EntryUrl>>
{
    private readonly EntryUrlRepository _entryUrlRepository;

    public List(EntryUrlRepository entryUrlRepository)
    {
        _entryUrlRepository = entryUrlRepository;
    }

    [HttpGet("/api/entries/{entryId}/urls")]
    [SwaggerOperation(OperationId = "EntryUrl.List", Tags = new[] {"EntryUrl"})]
    public override async Task<ActionResult<List<EntryUrl>>> HandleAsync(
        [FromMultiSource] EntryInfoListRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var eInfos = await _entryUrlRepository.PaginateListAsync(request, cancellationToken);
        return Ok(eInfos);
    }
}