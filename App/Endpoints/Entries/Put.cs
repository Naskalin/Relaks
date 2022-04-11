using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult
{
    private readonly EntryRepository _entryRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Put(EntryRepository entryRepository, IOptions<ApiBehaviorOptions> apiOptions)
    {
        _entryRepository = entryRepository;
        _apiOptions = apiOptions;
    }

    [HttpPut("/api/entries/{entryId}")]
    [SwaggerOperation(OperationId = "Entry.Put", Tags = new[] {"Entry"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] PutRequest putRequest,
        CancellationToken cancellationToken = new())
    {
        var validation = await new CreateRequestValidator().ValidateAsync(putRequest.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        var entry = await _entryRepository.FindByIdAsync(putRequest.EntryId, cancellationToken);
        if (entry == null) return NotFound();
        
        putRequest.MapTo(entry);
        entry.UpdatedAt = DateTime.UtcNow;
        await _entryRepository.UpdateAsync(entry, cancellationToken);

        return NoContent();
    }
}