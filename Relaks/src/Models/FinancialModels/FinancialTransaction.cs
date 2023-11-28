namespace Relaks.Models.FinancialModels;

public class FinancialTransaction
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// true Пополнение, false списание
    /// </summary>
    public bool IsPlus { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Описание операции
    /// </summary>
    public string? Description { get; set; }

    public Guid AccountId { get; set; } = default!;
    public FinancialAccount Account { get; set; } = null!;
    
    public Guid EntryId { get; set; } = default!;
    public BaseEntry Entry { get; set; } = null!;

    public List<FinancialTransactionItem> Items { get; set; } = new();
}