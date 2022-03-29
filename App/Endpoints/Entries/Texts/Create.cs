using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.Texts;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult
{
    private readonly EntryRepository _entryRepository;
    private readonly EntryTextRepository _entryTextRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Create(
        EntryRepository entryRepository,
        EntryTextRepository entryTextRepository,
        IOptions<ApiBehaviorOptions> apiOptions)
    {
        _entryRepository = entryRepository;
        _entryTextRepository = entryTextRepository;
        _apiOptions = apiOptions;
    }

    [HttpPost("/api/entries/{EntryId}/texts")]
    [SwaggerOperation(OperationId = "EntryText.Create", Tags = new[] {"EntryText"})]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] CreateRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var validation = await new CreateRequestValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            // return BadRequest(validation);
        }

        var entry = await _entryRepository.FindByIdAsync(request.EntryId, cancellationToken);
        if (entry == null)
        {
            return NotFound();
        }

        var entryText = new EntryText()
        {
            EntryId = entry.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        request.MapTo(entryText);
        
        await _entryTextRepository.CreateAsync(entryText, cancellationToken);
        
        entry.UpdatedAt = DateTime.UtcNow;
        await _entryRepository.UpdateAsync(entry, cancellationToken);
        return CreatedAtRoute("Entries_Texts_Get", new {EntryId = entryText.EntryId, EntryTextId = entryText.Id}, entryText);
    }
}