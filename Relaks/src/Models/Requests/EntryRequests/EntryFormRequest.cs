namespace Relaks.Models.Requests.EntryRequests;

public class EntryFormRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Discriminator { get; set; } = null!;
    public int Reputation { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
}

public class EntryCreateRequest : EntryFormRequest {}

public class EntryUpdateRequest : EntryFormRequest
{
    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
}