using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult
{
    private readonly EntryInfoDateRepository _infoDateRepository;

    public Put(EntryInfoDateRepository infoDateRepository)
    {
        _infoDateRepository = infoDateRepository;
    }

    [HttpPut("/api/entries/{entryId}/dates/{entryInfoId}")]
    [SwaggerOperation(OperationId = "EntryDate.Put", Tags = new[] {"EntryDate"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] PutRequest request,
        CancellationToken cancellationToken = new())
    {
        var validation = await new FormRequestValidator().ValidateAsync(request.Details, cancellationToken);
         if (!validation.IsValid)
         {
             return BadRequest(validation);
         }
         
         var eInfo = await _infoDateRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
         if (eInfo == null || eInfo.EntryId != request.EntryId)
         {
             return NotFound();
         }

         request.Details.MapTo(eInfo);
         eInfo.UpdatedAt = DateTime.UtcNow;
         await _infoDateRepository.UpdateAsync(eInfo, cancellationToken);

         return NoContent();
    }
}