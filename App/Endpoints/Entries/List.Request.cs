using App.Endpoints;
using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries;

public class EntryListRequest : BaseListDeletableRequest
{
    public EntryTypeEnum? EntryType { get; set; }
}