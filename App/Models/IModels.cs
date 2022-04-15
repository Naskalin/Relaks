namespace App.Models;

public interface IBaseEntity
{
    public Guid Id { get; set; }
}

// public interface IActualResource
// {
//     // public DateTime ActualStartAt { get; set; }
//     // public DateTime? ActualEndAt { get; set; }
//     // public string ActualStartAtReason { get; set; }
//     public string ActualEndAtReason { get; set; }
// }

public interface ITimestampResource
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public interface ISoftDelete
{
    public DateTime? DeletedAt { get; set; }
    public string DeletedReason { get; set; }
}

public interface IFtsEntity
{
    public int RowId { get; set; }
    public string Match { get; set; }
    public double? Rank { get; set; }
    
    public Guid Id { get; set; }
}