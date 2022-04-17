namespace App.Models;

public interface IBaseEntity
{
    public Guid Id { get; set; }
}

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
    // Не используется, но необходимо для объявления первичного ключа, чтобы не ругался ef core 
    public int RowId { get; set; }
    public string Match { get; set; }
    public double? Rank { get; set; }
    
    // Используется в качестве первичного ключа
    public Guid Id { get; set; }
}