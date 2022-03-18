using App.Models;

namespace App.Endpoints.Entries;

public class CreateRequest : IActualResource
{
    public string Name { get; set; } = null!;
    public string EntryType { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Reputation { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public DateTime ActualStartAt { get; set; }
    public DateTime? ActualEndAt { get; set; }
    public string ActualStartAtReason { get; set; } = null!;
    public string ActualEndAtReason { get; set; } = null!;
}