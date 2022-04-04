using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Phone;

public class Get : EndpointBaseAsync
    .WithRequest<EntryInfoGetRequest>
    .WithActionResult<EntryPhone>
{
    private readonly EntryPhoneRepository _entryPhoneRepository;

    public Get(EntryPhoneRepository entryPhoneRepository)
    {
        _entryPhoneRepository = entryPhoneRepository;
    }

    [HttpGet("/api/entries/{entryId}/phones/{entryInfoId}", Name = "EntryPhone_Get")]
    [SwaggerOperation(OperationId = "EntryPhone.Get", Tags = new[] {"EntryPhone"})]
    public override async Task<ActionResult<EntryPhone>> HandleAsync(
        [FromMultiSource] EntryInfoGetRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var eInfo = await _entryPhoneRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        return Ok(eInfo);
    }
}