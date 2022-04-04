using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Url;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult
{
    private readonly EntryUrlRepository _entryUrlRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Put(EntryUrlRepository entryUrlRepository, IOptions<ApiBehaviorOptions> apiOptions)
    {
        _entryUrlRepository = entryUrlRepository;
        _apiOptions = apiOptions;
    }

    [HttpPut("/api/entries/{entryId}/urls/{entryInfoId}")]
    [SwaggerOperation(OperationId = "EntryUrl.Put", Tags = new[] {"EntryUrl"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] PutRequest request, 
        CancellationToken cancellationToken = new())
    {
        var validation = await new FormRequestValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
        
        var eInfo = await _entryUrlRepository.FindByIdAsync(request.EntryInfoId, cancellationToken);
        if (eInfo == null || eInfo.EntryId != request.EntryId)
        {
            return NotFound();
        }
        
        request.Details.MapTo(eInfo);
        eInfo.UpdatedAt = DateTime.UtcNow;
        await _entryUrlRepository.UpdateAsync(eInfo, cancellationToken);

        return NoContent();
    }
}