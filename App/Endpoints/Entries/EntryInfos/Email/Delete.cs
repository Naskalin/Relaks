using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Email;

public class Delete : EndpointBaseAsync
    .WithRequest<EntryInfoDeleteRequest>
    .WithActionResult
{
    private readonly EntryEmailRepository _entryEmailRepository;

    public Delete(EntryEmailRepository entryEmailRepository)
    {
        _entryEmailRepository = entryEmailRepository;
    }

    [HttpDelete("/api/entries/{entryId}/emails/{entryInfoId}")]
    [SwaggerOperation(OperationId = "EntryEmail.Delete", Tags = new[] {"EntryEmail"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] EntryInfoDeleteRequest request,
        CancellationToken cancellationToken = new())
    {
        var eInfo = await _entryEmailRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        if (request.IsFullDelete == true)
        {
            await _entryEmailRepository.DeleteAsync(eInfo, cancellationToken);
        }
        else
        {
            request.MapTo(eInfo);
            await _entryEmailRepository.UpdateAsync(eInfo, cancellationToken);   
        }
        
        return NoContent();
    }
}