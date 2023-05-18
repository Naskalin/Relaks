using Relaks.Interfaces;

namespace Relaks.Models.Requests.EntryRequests;

public class EntryFormRequest : IEntry
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Discriminator { get; set; } = null!;
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public bool StartAtWithTime { get; set; }
    public bool EndAtWithTime { get; set; }
}

public class EntryCreateRequest : EntryFormRequest {}

public class EntryUpdateRequest : EntryFormRequest, ISoftDeletedReason
{
    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
}