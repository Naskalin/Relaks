using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.Texts;

public class ListRequest : BaseListRequest
{
    [FromRoute]
    public Guid EntryId { get; set; }

    [FromQuery]
    public string TextType { get; set; } = null!;
}