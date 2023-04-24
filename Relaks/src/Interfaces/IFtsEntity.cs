namespace Relaks.Interfaces;

public interface IFtsEntity
{
    /// <summary>
    /// Не используется, но необходимо для объявления первичного ключа, чтобы не ругался ef core 
    /// </summary>
    public int RowId { get; set; }
    
    /// <summary>
    /// NotMapped
    /// Только для операций с бд
    /// </summary>
    public string Match { get; set; }
    
    /// <summary>
    /// NotMapped
    /// </summary>
    public string Snippet { get; set; }
    
    /// <summary>
    /// NotMapped
    /// </summary>
    public double? Rank { get; set; }
    
    /// <summary>
    /// Идентификатор связанной сущности
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Содержимое
    /// </summary>
    public string Body { get; set; }
}