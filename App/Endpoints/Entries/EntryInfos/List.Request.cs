using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos;

public class EInfoListRequest : BaseListRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }
}