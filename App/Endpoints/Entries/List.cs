﻿using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    .WithActionResult<List<EntryDto>>
{
    private readonly EntryRepository _entryRepository;

    public List(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    [HttpGet("/api/entries")]
    [SwaggerOperation(OperationId = "Entry.List", Tags = new[] {"Entry"})]
    public override async Task<ActionResult<List<EntryDto>>> HandleAsync(
        [FromQuery] ListRequest listRequest,
        CancellationToken cancellationToken = new())
    {
        var query = _entryRepository.FindByListRequest(listRequest);
        return await Task.FromResult<ActionResult>(Ok(query.ToList()));
    }
}