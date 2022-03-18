using App.Mappers;
using App.Models;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.Dates;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequest>
    .WithActionResult<EntryDate>
{
    private readonly EntryDateRepository _entryDateRepository;
    private readonly EntryRepository _entryRepository;

    public Create(EntryDateRepository entryDateRepository, EntryRepository entryRepository)
    {
        _entryDateRepository = entryDateRepository;
        _entryRepository = entryRepository;
    }

    [HttpPost("/api/entries/{EntryId}/dates")]
    public override async Task<ActionResult<EntryDate>> HandleAsync(
        [FromMultiSource] CreateRequest createRequest,
        CancellationToken cancellationToken = new())
    {
        var validation = await new CreateRequestValidator().ValidateAsync(createRequest.Details, cancellationToken);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }

        var entry = await _entryRepository.FindByIdAsync(createRequest.EntryId, cancellationToken);
        if (entry == null)
        {
            return NotFound();
        }

        var entryDate = new EntryDate()
        {
            EntryId = entry.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        createRequest.MapTo(entryDate);
        await _entryDateRepository.CreateAsync(entryDate, cancellationToken);


        return CreatedAtRoute("Entries_Dates_Get",
            new {EntryId = entryDate.EntryId, EntryDateId = entryDate.Id}, entryDate);
    }
}