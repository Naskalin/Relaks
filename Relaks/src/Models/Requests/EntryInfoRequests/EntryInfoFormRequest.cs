using Relaks.Interfaces;

namespace Relaks.Models.Requests.EntryInfoRequests;

public class EntryInfoFormRequest : IEntryInfo
{
    public string? Title { get; set; }
    public bool IsFavorite { get; set; }
    public string Discriminator { get; set; } = null!;
    
    //EiEmail
    public string? Email { get; set; }
    
    //EiPhone
    public string? Number { get; set; }
    public string? Region { get; set; }

    //EiDate
    public DateTime? Date { get; set; }
    public bool? WithTime { get; set; }
    
    //EiUrl
    public string? Url { get; set; }
}

public class EntryInfoCreateRequest : EntryInfoFormRequest {}

public class EntryInfoUpdateRequest : EntryInfoFormRequest, ISoftDeletedReason
{
    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
}