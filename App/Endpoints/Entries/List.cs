using App.Models;
using App.Repository;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace App.Endpoints.Entries;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    // .WithActionResult<List<EntryFtsDto>>
    .WithActionResult
{
    private readonly EntryRepository _entryRepository;

    public List(EntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    [HttpGet("/api/entries")]
    [SwaggerOperation(OperationId = "Entry.List", Tags = new[] {"Entry"})]
    // public override async Task<ActionResult<List<EntryFtsDto>>> HandleAsync(
    public override async Task<ActionResult> HandleAsync(
        [FromQuery] ListRequest listRequest,
        CancellationToken cancellationToken = new())
    {
        var query = _entryRepository.FindByListRequest(listRequest);
        
        // .ToListAsync(cancellationToken);
        return Ok(query.ToList());
        
        // if (!String.IsNullOrEmpty(listRequest.Search))
        // {
        //     var entries = _entryRepository.FindFtsResults(listRequest.Search);
        //
        //     var results = entries.Join(
        //         _entryRepository.Entities,
        //         x => x.Id,
        //         e => e.Id,
        //         (result, entry) => MakeResult(result, entry)
        //     );
        //
        //     return Ok(results);
        // }

        // return Ok();
    }
    
    // public class EntityFtsResult : Entry
    // {
    //     public string? Snippet { get; set; }
    // }
    //
    // private static EntityFtsResult MakeResult(FtsResult ftsResult, Entry entry)
    // {
    //     var r = (EntityFtsResult) entry;
    //     r.Snippet = ftsResult.Snippet;
    //
    //     return r;
    // }
}