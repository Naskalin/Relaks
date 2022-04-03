using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries.EntryInfos.Email;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult
{
    private readonly EntryRepository _entryRepository;
    private readonly EntryEmailRepository _entryEmailRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Create(
        EntryRepository entryRepository,
        EntryEmailRepository entryEmailRepository,
        IOptions<ApiBehaviorOptions> apiOptions
    )
    {
        _entryRepository = entryRepository;
        _entryEmailRepository = entryEmailRepository;
        _apiOptions = apiOptions;
    }

    [HttpPost("/api/entries/{entryId}/emails")]
    [SwaggerOperation(OperationId = "EntryEmail.Create", Tags = new[] {"EntryEmail"})]
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

        var eInfo = new EntryEmail {EntryId = entry.Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow};
        request.Details.MapTo(eInfo);
        await _entryEmailRepository.CreateAsync(eInfo, cancellationToken);

        return CreatedAtRoute("Entries_Emails_Get", new {entryId = entry.Id, entryInfoId = eInfo.Id}, eInfo);
    }
}