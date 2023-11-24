namespace Relaks.Models.FinancialModels;

public class FinancialAccount
{
    public Guid Id { get; } = Guid.NewGuid();
    public required string Title { get; set; }
    public string? Description { get; set; }
    
    public required string CurrencyId { get; set; }
    public required Currency Currency { get; set; }
    
    public Guid? CategoryId { get; set; }
    public FinancialAccountCategory? Category { get; set; }
    
    /// <summary>
    /// Дата открытия счёта
    /// </summary>
    public required DateTime StartAt { get; set; }
    
    /// <summary>
    /// Дата закрытия счёта
    /// </summary>
    public DateTime? EndAt { get; set; }
    
    /// <summary>
    /// Владелец счёта
    /// </summary>
    public required Guid EntryId { get; set; }
    public required BaseEntry Entry { get; set; }
}