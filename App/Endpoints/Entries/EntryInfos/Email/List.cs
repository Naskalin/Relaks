using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Email;

public class List : EndpointBaseAsync
    .WithRequest<EInfoListRequest>
    .WithActionResult<List<EntryEmail>>
{
    private readonly EntryEmailRepository _entryEmailRepository;

    public List(EntryEmailRepository entryEmailRepository)
    {
        _entryEmailRepository = entryEmailRepository;
    }

    [HttpGet("/api/entries/{entryId}/emails")]
    [SwaggerOperation(OperationId = "EntryEmail.List", Tags = new[] {"EntryEmail"})]
    public override async Task<ActionResult<List<EntryEmail>>> HandleAsync(
        [FromMultiSource] EInfoListRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var eInfos = await _entryEmailRepository.PaginateListAsync(request, cancellationToken);
        return Ok(eInfos);
    }
}