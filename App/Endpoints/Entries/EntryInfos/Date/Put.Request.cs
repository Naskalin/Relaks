using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos.Date;

public class PutRequest : CreateRequest
{
    [FromRoute] public Guid EntryInfoId { get; set; }
}