using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Structures;

public class GetRequest
{
    [FromRoute] public Guid EntryId { get; set; }
    [FromRoute] public Guid StructureId { get; set; }
}