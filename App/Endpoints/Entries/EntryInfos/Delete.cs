using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos;

public class Delete : EndpointBaseAsync
    .WithRequest<EntryInfoGetRequest>
    .WithActionResult
{
    private readonly EntryInfoRepository _entryInfoRepository;

    public Delete(EntryInfoRepository entryInfoRepository)
    {
        _entryInfoRepository = entryInfoRepository;
    }

    [HttpDelete("/api/entries/{entryId}/entry-infos/{entryInfoId}")]
    [SwaggerOperation(OperationId = "EntryInfo.Delete", Tags = new[] {"EntryInfo"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] EntryInfoGetRequest request,
        CancellationToken cancellationToken = new())
    {
        var eInfo = await _entryInfoRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        await _entryInfoRepository.DeleteAsync(eInfo, cancellationToken);
        return NoContent();
    }
}