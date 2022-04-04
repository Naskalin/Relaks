using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class Get : EndpointBaseAsync
    .WithRequest<EntryInfoGetRequest>
    .WithActionResult<EntryDate>
{
    private readonly EntryDateRepository _entryDateRepository;

    public Get(EntryDateRepository entryDateRepository)
    {
        _entryDateRepository = entryDateRepository;
    }

    [HttpGet("/api/entries/{entryId}/dates/{entryInfoId}", Name = "EntryDate_Get")]
    [SwaggerOperation(OperationId = "EntryDate.Get", Tags = new [] {"EntryDate"})]
    public override async Task<ActionResult<EntryDate>> HandleAsync(
        [FromMultiSource] EntryInfoGetRequest request,
        CancellationToken cancellationToken = new())
    {
        var eInfo = await _entryDateRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        return Ok(eInfo);
    }
}