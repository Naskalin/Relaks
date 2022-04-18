using App.Mappers;
using App.Models;
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult<Entry>
{
    private readonly EntryRepository _entryRepository;
    private readonly IOptions<ApiBehaviorOptions> _apiOptions;

    public Create(EntryRepository entryRepository, IOptions<ApiBehaviorOptions> apiOptions)
    {
        _entryRepository = entryRepository;
        _apiOptions = apiOptions;
    }

    [HttpPost("/api/entries")]
    [SwaggerOperation(OperationId = "Entry.Create", Tags = new[] {"Entry"})]
    public override async Task<ActionResult<Entry>> HandleAsync(
        [FromBody] CreateRequest createRequest,
        CancellationToken cancellationToken = new())
    {
        var validation = await new CreateRequestValidator().ValidateAsync(createRequest, cancellationToken);
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return (ActionResult) _apiOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        var entry = new Entry()
        {
            CreatedAt = DateTime.UtcNow,
        };
        createRequest.MapTo(entry);

        await _entryRepository.CreateAsync(entry, cancellationToken);
        return CreatedAtRoute("Entry_Get", new {entryId = entry.Id}, entry);
    }
}