using App.Models;

namespace App.Endpoints.Entries.EntryInfos;

public interface IEntryInfoFormCommonRequest : ISoftDelete
{
    public string Title { get; set; }
}