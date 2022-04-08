using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Phone;

public class Delete : EndpointBaseAsync
    .WithRequest<EntryInfoDeleteRequest>
    .WithActionResult
{
    private readonly EntryPhoneRepository _entryPhoneRepository;

    public Delete(EntryPhoneRepository entryPhoneRepository)
    {
        _entryPhoneRepository = entryPhoneRepository;
    }

    [HttpDelete("/api/entries/{entryId}/phones/{entryInfoId}")]
    [SwaggerOperation(OperationId = "EntryPhone.Delete", Tags = new[] {"EntryPhone"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] EntryInfoDeleteRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var eInfo = await _entryPhoneRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }
        
        if (request.IsFullDelete == true)
        {
            await _entryPhoneRepository.DeleteAsync(eInfo, cancellationToken);
        }
        else
        {
            request.MapTo(eInfo);
            await _entryPhoneRepository.UpdateAsync(eInfo, cancellationToken);   
        }
        return NoContent();
    }
}