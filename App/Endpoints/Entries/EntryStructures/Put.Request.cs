using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryStructures;

public class PutRequest : CreateRequest
{
    [FromRoute] public Guid StructureId { get; set; }
}