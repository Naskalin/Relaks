using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.Dates;

public class ListRequest : BaseListRequest
{
    [FromRoute] public Guid EntryId { get; set; }
}