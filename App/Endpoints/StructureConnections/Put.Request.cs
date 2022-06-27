using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.StructureConnections;

public class PutRequest : CreateRequest
{
    [FromRoute]
    public Guid StructureConnectionId { get; set; }
}