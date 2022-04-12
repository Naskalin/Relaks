using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Files;

public class FileRequest
{
    [FromRoute]
    public Guid FileId { get; set; }
    
    [FromQuery]
    public string? ImageFilter { get; set; }
}