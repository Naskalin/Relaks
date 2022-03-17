
namespace App.Models;

public interface IActualResource
{
    public DateTime ActualStartAt { get; set; }
    public DateTime? ActualEndAt { get; set; }
    public string ActualStartAtReason { get; set; }
    public string ActualEndAtReason { get; set; }
}

public interface ITimestampResource
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}