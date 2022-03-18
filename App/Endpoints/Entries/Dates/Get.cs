using App.Models;
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.Dates;

public class Get : EndpointBaseAsync
    .WithRequest<GetRequest>
    .WithActionResult<EntryDate>
{
    private readonly EntryDateRepository _entryDateRepository;

    public Get(EntryDateRepository entryDateRepository)
    {
        _entryDateRepository = entryDateRepository;
    }

    [HttpGet("/api/entries/{EntryId}/dates/{EntryDateId}", Name = "Entries_Dates_Get")]
    public override async Task<ActionResult<EntryDate>> HandleAsync(
        [FromRoute] GetRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var entryDate = await _entryDateRepository.FindByIdAsync(request.EntryDateId, cancellationToken);
        if (entryDate == null || entryDate.EntryId != request.EntryId)
        {
            return NotFound();
        }

        return Ok(entryDate);
    }
}