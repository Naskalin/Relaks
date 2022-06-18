using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.StructureItems;

public class PutRequest : CreateRequest
{
    [FromRoute]
    public Guid StructureItemId { get; set; }
}