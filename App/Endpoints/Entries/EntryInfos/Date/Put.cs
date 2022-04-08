using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult
{
    private readonly EntryDateRepository _entryDateRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Put(EntryDateRepository entryDateRepository, IOptions<ApiBehaviorOptions> apiOptions)
    {
        _entryDateRepository = entryDateRepository;
        _apiOptions = apiOptions;
    }

    [HttpPut("/api/entries/{entryId}/dates/{entryInfoId}")]
    [SwaggerOperation(OperationId = "EntryDate.Put", Tags = new[] {"EntryDate"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] PutRequest request,
        CancellationToken cancellationToken = new())
    {
        var validation = await new PutRequestValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        var eInfo = await _entryDateRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }

        request.Details.MapTo(eInfo);
        eInfo.UpdatedAt = DateTime.UtcNow;
        await _entryDateRepository.UpdateAsync(eInfo, cancellationToken);

        return NoContent();
    }
}