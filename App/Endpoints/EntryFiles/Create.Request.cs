using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryFiles;

public class CreateRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }

    [FromForm]
    public List<IFormFile> Files { get; set; } = null!;

    [FromForm]
    public string? Category { get; set; }
}