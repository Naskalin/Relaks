using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class List : EndpointBaseAsync
    .WithRequest<EInfoListRequest>
    .WithActionResult<List<EntryDate>>
{
    private readonly EntryDateRepository _entryDateRepository;

    public List(EntryDateRepository entryDateRepository)
    {
        _entryDateRepository = entryDateRepository;
    }

    [HttpGet("/api/entries/{entryId}/dates")]
    [SwaggerOperation(OperationId = "EntryDate.List", Tags = new[] {"EntryDate"})]
    public override async Task<ActionResult<List<EntryDate>>> HandleAsync(
        [FromMultiSource] EInfoListRequest request,
        CancellationToken cancellationToken = new())
    {
        var eInfos = await _entryDateRepository.PaginateListAsync(request, cancellationToken);
        return Ok(eInfos);
    }
}