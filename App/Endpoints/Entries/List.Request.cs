using App.Endpoints;
using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Endpoints.Entries;

public class ListRequest : BaseListRequest
{
    public EntryTypeEnum? EntryType { get; set; }
}