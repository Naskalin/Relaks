using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.Dates;

public class GetRequest
{
    [FromRoute] public Guid EntryId { get; set; }
    [FromRoute] public Guid EntryDateId { get; set; }
}