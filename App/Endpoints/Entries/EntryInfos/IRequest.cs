using App.Models;

namespace App.Endpoints.Entries.EntryInfos;

public interface IEntryInfoFormCommonRequest : ISoftDelete
{
    public string Title { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; }
}