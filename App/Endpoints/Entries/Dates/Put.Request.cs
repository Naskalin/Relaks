using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.Dates;

public class PutRequest : CreateRequest
{
    [FromRoute] public Guid EntryDateId { get; set; }
}