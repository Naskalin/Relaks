using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryStructures;

public class GetRequest
{
    [FromRoute] public Guid EntryId { get; set; }
    [FromRoute] public Guid StructureId { get; set; }
}