using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos;

public class EntryInfoGetRequest
{
    [FromRoute] public Guid EntryId { get; set; }
    [FromRoute] public Guid EntryInfoId { get; set; }
}