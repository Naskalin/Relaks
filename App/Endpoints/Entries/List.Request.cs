using App.Endpoints.Base;
using App.Models;

namespace App.Endpoints.Entries;

public class ListRequest : BaseRequest
{
    public EntryTypeEnum? EntryType { get; set; }
}