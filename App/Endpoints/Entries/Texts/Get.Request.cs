using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.Texts;

public class GetRequest
{
    [FromRoute] public Guid EntryId { get; set; }
    [FromRoute] public Guid EntryTextId { get; set; }
}