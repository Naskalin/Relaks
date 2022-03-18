using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.Texts;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult<EntryText>
{
    private readonly EntryRepository _entryRepository;
    private readonly EntryTextRepository _entryTextRepository;

    public Create(EntryRepository entryRepository, EntryTextRepository entryTextRepository)
    {
        _entryRepository = entryRepository;
        _entryTextRepository = entryTextRepository;
    }

    [HttpPost("/api/entries/{EntryId}/texts")]
    public override async Task<ActionResult<EntryText>> HandleAsync(
        [FromMultiSource] CreateRequest request,
        CancellationToken cancellationToken = new()
    )
    {
        var validation = await new CreateRequestValidator().ValidateAsync(request.Details, cancellationToken);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
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
        return Ok(entryText);
    }
}