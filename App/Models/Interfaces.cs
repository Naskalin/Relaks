using JsonApiDotNetCore.Resources.Annotations;

namespace App.Models;

public interface IActualResource
{
    public DateTime ActualFrom { get; set; }
    public DateTime? ActualTo { get; set; }
    public string? ActualToReason { get; set; }
    public string? ActualFromReason { get; set; }
}

public interface ITimestampResource
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}