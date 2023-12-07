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
    /// Сумма FinancialTransactionItem
    /// </summary>
    [Precision(19, 4)]
    public decimal Total { get; set; }

    public void UpdateTotal() => Total = Items.Sum(x => x.Amount);

    /// <summary>
    /// Баланс после транзакции
    /// </summary>
    [Precision(19, 4)]
    public decimal Balance { get; set; }
    
    public void UpdateBalance(decimal fromBalance)
    {
        Balance = IsPlus ? fromBalance + Total : fromBalance - Total;
    }
    
    /// <summary>
    /// Баланс до транзакции
    /// </summary>
    /// <returns></returns>
    public decimal FromBalance()
    {
        return IsPlus ? Balance - Total : Balance + Total;
    }
}