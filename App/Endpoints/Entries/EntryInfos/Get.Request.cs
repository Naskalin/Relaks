using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos;

public class EInfoGetRequest
{
    [FromRoute] public Guid EntryId { get; set; }
    [FromRoute] public Guid EntryInfoId { get; set; }
}