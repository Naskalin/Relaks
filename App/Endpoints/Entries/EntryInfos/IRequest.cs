namespace App.Endpoints.Entries.EntryInfos;

public interface IEntryInfoFormCommonRequest
{
    public string Title { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; }
}

// public interface IEntryInfoListRequest
// {
//     public string Title { get; set; }
//     public DateTime? DeletedAt { get; set; }
//     public string DeletedReason { get; set; }
// }