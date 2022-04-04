using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos.Url;

public class PutRequest : CreateRequest
{
    [FromRoute] public Guid EntryInfoId { get; set; }
}