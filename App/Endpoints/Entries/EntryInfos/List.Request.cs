using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos;

public class EntryInfoListRequest : BaseListRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }
    
    [FromQuery]
    public EntryInfoType? Type { get; set; }
}