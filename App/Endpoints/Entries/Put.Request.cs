using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries;

public class PutRequest
{
    [FromRoute]
    [Required]
    public Guid Id { get; set; }

    [FromBody]
    public CreateRequest Details { get; set; } = null!;
}