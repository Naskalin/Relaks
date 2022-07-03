using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos;

public class EntryInfoListDeletableRequest : BaseListDeletableRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }

    [FromQuery]
    public List<string>? Type { get; set; }
}