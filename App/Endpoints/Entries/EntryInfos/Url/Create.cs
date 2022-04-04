using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Url;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult
{
    private readonly EntryUrlRepository _entryUrlRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;
    private readonly EntryRepository _entryRepository;

    public Create(
        EntryUrlRepository entryUrlRepository,
        IOptions<ApiBehaviorOptions> apiOptions,
        EntryRepository entryRepository
    )
    {
        _entryUrlRepository = entryUrlRepository;
        _apiOptions = apiOptions;
        _entryRepository = entryRepository;
    }
    
    [HttpPost("/api/entries/{entryId}/urls")]
    [SwaggerOperation(OperationId = "EntryUrl.Create", Tags = new[] {"EntryUrl"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] CreateRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var validation = await new FormRequestValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
        
        var entry = await _entryRepository.FindByIdAsync(request.EntryId, cancellationToken);
        if (entry == null)
        {
            return NotFound();
        }
        
        var eInfo = new EntryUrl() {EntryId = entry.Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow};
        request.Details.MapTo(eInfo);
        await _entryUrlRepository.CreateAsync(eInfo, cancellationToken);
        
        entry.UpdatedAt = DateTime.UtcNow;
        await _entryRepository.UpdateAsync(entry, cancellationToken);
        
        return CreatedAtRoute("EntryUrl_Get", new {entryId = entry.Id, entryInfoId = eInfo.Id}, eInfo);
    }
}