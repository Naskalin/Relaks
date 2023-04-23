namespace Relaks.Interfaces;

public interface IFtsEntity
{
    /// <summary>
    /// Не используется, но необходимо для объявления первичного ключа, чтобы не ругался ef core 
    /// </summary>
    public int RowId { get; set; }
    public string Match { get; set; }
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