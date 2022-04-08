using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class PutRequest
{
    [FromRoute]
    [Required]
    public Guid EntryId { get; set; }

    [FromRoute]
    [Required]
    public Guid EntryInfoId { get; set; }

    [FromBody]
    public EntryDatePutRequestDetails Details { get; set; } = null!;
}

public class EntryDatePutRequestDetails : EntryDateCreateRequestDetails, ISoftDeleteRequest
{
    public bool? IsFullDelete { get; set; }
    public string DeletedReason { get; set; } = null!;
}