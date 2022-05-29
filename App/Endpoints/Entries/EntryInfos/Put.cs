using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult
{
    private readonly EntryInfoRepository _entryInfoRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Put(EntryInfoRepository entryInfoRepository, IOptions<ApiBehaviorOptions> apiOptions)
    {
        _entryInfoRepository = entryInfoRepository;
        _apiOptions = apiOptions;
    }

    [HttpPut("/api/entries/{entryId}/entry-infos/{entryInfoId}")]
    [SwaggerOperation(OperationId = "EntryInfo.Put", Tags = new[] {"EntryInfo"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] PutRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var validation = await new RequestDetailsValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        var eInfo = await _entryInfoRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        request.Details.MapTo(eInfo);
        await _entryInfoRepository.UpdateAsync(eInfo, cancellationToken);

        return NoContent();
    }
}