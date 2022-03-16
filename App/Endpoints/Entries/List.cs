using App.DbConfigurations;
using App.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Endpoints.Entries;

public class List : EndpointBaseAsync
    .WithRequest<ListRequest>
    .WithActionResult<List<Entry>>
{
    private readonly AppDbContext _db;

    public List(AppDbContext db)
    {
        _db = db;
    }
    
    [HttpGet("/api/entries")]
    public override async Task<ActionResult<List<Entry>>> HandleAsync([FromQuery] ListRequest request, CancellationToken cancellationToken = new())
    {
        var entries = await _db.Entries.Take(50).ToListAsync(cancellationToken: cancellationToken);
        return Ok(entries);
    }
}