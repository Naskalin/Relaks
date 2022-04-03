using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class List : EndpointBaseAsync
    .WithRequest<EntryInfoListRequest>
    .WithActionResult<List<EntryInfoDate>>
{
    private readonly EntryInfoDateRepository _infoDateRepository;

    public List(EntryInfoDateRepository infoDateRepository)
    {
        _infoDateRepository = infoDateRepository;
    }

    [HttpGet("/api/entries/{entryId}/dates")]
    [SwaggerOperation(OperationId = "EntryDate.List", Tags = new[] {"EntryDate"})]
    public override async Task<ActionResult<List<EntryInfoDate>>> HandleAsync(
        [FromMultiSource] EntryInfoListRequest request,
        CancellationToken cancellationToken = new())
    {
        var eInfos = await _infoDateRepository.PaginateListAsync(request, cancellationToken);
        return Ok(eInfos);
    }
}