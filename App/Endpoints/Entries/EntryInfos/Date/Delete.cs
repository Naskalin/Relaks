using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class Delete : EndpointBaseAsync
    .WithRequest<EntryInfoDeleteRequest>
    .WithActionResult
{
    private readonly EntryDateRepository _entryDateRepository;

    public Delete(EntryDateRepository entryDateRepository)
    {
        _entryDateRepository = entryDateRepository;
    }

    [HttpDelete("/api/entries/{entryId}/dates/{entryInfoId}")]
    [SwaggerOperation(OperationId = "EntryDate.Delete", Tags = new[] {"EntryDate"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] EntryInfoDeleteRequest request,
        CancellationToken cancellationToken = new())
    {
        var eInfo = await _entryDateRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        if (request.IsFullDelete == true)
        {
            await _entryDateRepository.DeleteAsync(eInfo, cancellationToken);
        }
        else
        {
            request.MapTo(eInfo);
            await _entryDateRepository.UpdateAsync(eInfo, cancellationToken);   
        }
        
        return NoContent();
    }
}