using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class CreateRequest
{
    [FromRoute]
    [Required]
    public Guid EntryId { get; set; }

    [FromBody]
    public EntryDateCreateRequestDetails Details { get; set; } = null!;
}

public class EntryDateCreateRequestDetails : IEntryInfoFormCommonRequest
{
    public string Title { get; set; } = null!;
    public DateTime Date { get; set; }
}