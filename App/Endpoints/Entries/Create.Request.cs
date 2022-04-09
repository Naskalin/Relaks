using App.Models;

namespace App.Endpoints.Entries;

public class CreateRequest : ISoftDelete
{
    public string Name { get; set; } = null!;
    public string EntryType { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Reputation { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }

    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; } = null!;
}