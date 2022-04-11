using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos;

public class EntryInfoListRequest : BaseListRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }
}