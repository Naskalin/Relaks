using System.ComponentModel.DataAnnotations;
using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.Dates;

public class CreateRequest
{
    [FromRoute]
    [Required]
    public Guid EntryId { get; set; }

    [FromBody]
    public CreateRequestDetails Details { get; set; } = null!;
}

public class CreateRequestDetails : IActualResource
{
    public string About { get; set; } = null!;
    public DateTime Val { get; set; }
    public DateTime ActualStartAt { get; set; }
    public DateTime? ActualEndAt { get; set; }
    public string ActualEndAtReason { get; set; } = null!;
    public string ActualStartAtReason { get; set; } = null!;
}