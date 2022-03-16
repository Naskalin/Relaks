using System.ComponentModel.DataAnnotations;
using DotNext;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries;

public class PatchRequest
{
    [FromRoute]
    [Required]
    public Guid Id { get; set; }

    // [FromBody]
    // public string Name { get; set; } = null!;
    
    [FromBody]
    public UpdateDetails Details { get; set; } = null!;
    
    public class UpdateDetails
    {
        public Optional<string> Name { get; set; }
        public Optional<string> Description { get; set; }
    }
}