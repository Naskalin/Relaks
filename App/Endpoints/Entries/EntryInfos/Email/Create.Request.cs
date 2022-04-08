namespace App.Endpoints.Entries.EntryInfos.Email;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class CreateRequest
{
    [FromRoute]
    [Required]
    public Guid EntryId { get; set; }

    [FromBody]
    public EntryEmailCreateRequestDetails Details { get; set; } = null!;
}

public class EntryEmailCreateRequestDetails : IEntryInfoFormCommonRequest
{
    public string Title { get; set; } = null!;
    public string DeletedReason { get; set; } = null!;
    public string Email { get; set; } = null!;
}