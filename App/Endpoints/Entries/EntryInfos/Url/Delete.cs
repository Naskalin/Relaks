using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Url;

public class Delete : EndpointBaseAsync
    .WithRequest<EntryInfoDeleteRequest>
    .WithActionResult
{
    private readonly EntryUrlRepository _entryUrlRepository;

    public Delete(EntryUrlRepository entryUrlRepository)
    {
        _entryUrlRepository = entryUrlRepository;
    }

    [HttpDelete("/api/entries/{entryId}/urls/{entryInfoId}")]
    [SwaggerOperation(OperationId = "EntryUrl.Delete", Tags = new[] {"EntryUrl"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] EntryInfoDeleteRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var eInfo = await _entryUrlRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        if (request.IsFullDelete == true)
        {
            await _entryUrlRepository.DeleteAsync(eInfo, cancellationToken);
        }
        else
        {
            request.MapTo(eInfo);
            await _entryUrlRepository.UpdateAsync(eInfo, cancellationToken);   
        }
        return NoContent();
    }
}