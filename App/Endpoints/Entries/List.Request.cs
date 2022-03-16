using App.Endpoints.Base;
using App.Models;

namespace App.Endpoints.Entries;

public class ListRequest : BaseListRequest
{
    public EntryTypeEnum? EntryType { get; set; }
}