namespace Relaks.Interfaces;

public interface ISoftDeleted
{
    public DateTime? DeletedAt { get; set; }
}

public interface ISoftDeletedReason : ISoftDeleted
{
    public string? DeletedReason { get; set; }
}