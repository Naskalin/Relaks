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
    
    /// <summary>
    /// Баланс после транзакции
    /// </summary>
    [Precision(19, 4)]
    public decimal Balance { get; set; }

    public void UpdateBalance(decimal fromBalance)
    {
        var itemsTotal = Items.Sum(x => x.Amount);
        Balance = IsPlus ? fromBalance + itemsTotal : fromBalance - itemsTotal;
    }

    /// <summary>
    /// Баланс до транзакции
    /// </summary>
    /// <returns></returns>
    public decimal FromBalance()
    {
        var itemsTotal = Items.Sum(x => x.Amount);
        return IsPlus ? Balance - itemsTotal : Balance + itemsTotal;
    }
}