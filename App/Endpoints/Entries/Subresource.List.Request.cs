using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries;

public class EntrySubresourceListRequest : BaseListRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }
}