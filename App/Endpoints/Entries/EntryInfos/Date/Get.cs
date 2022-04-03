using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class Get : EndpointBaseAsync
    .WithRequest<GetRequest>
    .WithActionResult<EntryInfoDate>
{
    private readonly EntryInfoDateRepository _infoDateRepository;

    public Get(EntryInfoDateRepository infoDateRepository)
    {
        _infoDateRepository = infoDateRepository;
    }

    [HttpGet("/api/entries/{entryId}/dates/{entryInfoId}", Name = "Entries_Dates_Get")]
    [SwaggerOperation(OperationId = "EntryDate.Get", Tags = new [] {"EntryDate"})]
    public override async Task<ActionResult<EntryInfoDate>> HandleAsync(
        [FromMultiSource] GetRequest request,
        CancellationToken cancellationToken = new())
    {
        var eInfo = await _infoDateRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        return Ok(eInfo);
    }
}