using Microsoft.EntityFrameworkCore;

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

    public Guid AccountId { get; set; }
    public FinancialAccount Account { get; set; } = null!;
    
    public Guid EntryId { get; set; }
    public BaseEntry Entry { get; set; } = null!;

    public List<FinancialTransactionItem> Items { get; set; } = new();
    
    [Precision(19, 4)]
    public decimal FromBalance { get; set; }

    public decimal Balance()
    {
        var itemsTotal = Items.Sum(x => x.Amount);
        return IsPlus ? FromBalance + itemsTotal : FromBalance - itemsTotal;
    }
}