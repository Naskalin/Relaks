using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries.EntryInfos.Email;

public class PutRequest : CreateRequest
{
    [FromRoute] public Guid EntryInfoId { get; set; }
}