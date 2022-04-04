using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos.Url;

public class CreateRequest
{
    [FromRoute]
    [Required]
    public Guid EntryId { get; set; }

    [FromBody]
    public RequestUrlDetails Details { get; set; } = null!;
}

public class RequestUrlDetails : IEntryInfoFormCommonRequest
{
    public string Title { get; set; } = null!;
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
    public string Url { get; set; } = null!;
}