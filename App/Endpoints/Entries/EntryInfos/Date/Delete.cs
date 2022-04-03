using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class Delete : EndpointBaseAsync
    .WithRequest<GetRequest>
    .WithActionResult
{
    private readonly EntryInfoDateRepository _infoDateRepository;

    public Delete(EntryInfoDateRepository infoDateRepository)
    {
        _infoDateRepository = infoDateRepository;
    }

    [HttpDelete("/api/entries/{entryId}/dates/{entryInfoId}")]
    [SwaggerOperation(OperationId = "EntryDate.Delete", Tags = new[] {"EntryDate"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] GetRequest request,
        CancellationToken cancellationToken = new())
    {
        var eInfo = await _infoDateRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        await _infoDateRepository.DeleteAsync(eInfo, cancellationToken);
        return NoContent();
    }
}