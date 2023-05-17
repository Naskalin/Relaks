using Relaks.Interfaces;

namespace Relaks.Models.Misc;

public class MySoftDeletedReason : ISoftDeletedReason
{
    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
}