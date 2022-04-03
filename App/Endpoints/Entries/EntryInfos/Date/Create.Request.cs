namespace App.Endpoints.Entries.EntryInfos.Date;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class CreateRequest
{
    [FromRoute]
    [Required]
    public Guid EntryId { get; set; }

    [FromBody]
    public RequestDateDetails Details { get; set; } = null!;
}

public class RequestDateDetails : IEntryInfoFormCommonRequest
{
    public string Title { get; set; } = null!;
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
    public DateTime Date { get; set; }
}