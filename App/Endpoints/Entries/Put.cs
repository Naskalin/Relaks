﻿using App.Mappers;
using App.Repository;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries;

public class Put : EndpointBaseAsync
    .WithRequest<PutRequest>
    .WithActionResult
{
    private readonly EntryRepository _entryRepository;

    public Put(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    [HttpPut("/api/entries/{id}")]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] PutRequest putRequest,
        CancellationToken cancellationToken = new())
    {
        var validation = await new CreateRequestValidator().ValidateAsync(putRequest.Details, cancellationToken);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }
        
        var entry = await _entryRepository.FindSingleAsync(putRequest.Id, cancellationToken);
        if (entry == null) return NotFound();
        
        putRequest.MapTo(entry);
        entry.UpdatedAt = DateTime.UtcNow;
        await _entryRepository.UpdateAsync(entry, cancellationToken);

        return NoContent();
    }
}