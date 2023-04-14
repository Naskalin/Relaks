namespace Relaks.Interfaces;

public interface ISoftDeleted
{
    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
}