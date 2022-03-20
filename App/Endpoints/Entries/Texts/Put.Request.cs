using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.Texts;

public class PutRequest : CreateRequest
{
    [FromRoute] public Guid EntryTextId { get; set; }
}