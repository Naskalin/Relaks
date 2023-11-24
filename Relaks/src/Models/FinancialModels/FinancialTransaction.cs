namespace Relaks.Models.FinancialModels;

public class FinancialTransaction
{
    public Guid Id { get; } = Guid.NewGuid();
    
    /// <summary>
    /// true Пополнение, false списание
    /// </summary>
    public bool IsPlus { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Описание операции
    /// </summary>
    public string? Description { get; set; }
    
    public required Guid AccountId { get; set; }
    public required FinancialAccount Account { get; set; }
    
    public required Guid EntryId { get; set; }
    public required BaseEntry Entry { get; set; }

    public List<FinancialTransactionItem> Items { get; set; } = new();
}