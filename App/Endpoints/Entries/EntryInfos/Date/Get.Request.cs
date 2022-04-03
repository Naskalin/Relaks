using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class GetRequest
{
    [FromRoute] public Guid EntryId { get; set; }
    [FromRoute] public Guid EntryInfoId { get; set; }
}