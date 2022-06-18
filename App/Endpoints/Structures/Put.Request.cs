using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Structures;

public class PutRequest : CreateRequest
{
    [FromRoute] public Guid StructureId { get; set; }
}