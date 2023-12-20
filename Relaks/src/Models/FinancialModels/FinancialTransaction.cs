using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Relaks.Models.FinancialModels;

public abstract class BaseFinancialTransaction
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
    [MaxLength(500)]
    public string? Description { get; set; }

    public Guid AccountId { get; set; }
    public FinancialAccount Account { get; set; } = null!;
    
    /// <summary>
    /// Сумма FinancialTransactionItem
    /// </summary>
    [Precision(19, 4)]
    public decimal Total { get; set; }

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
    
    public List<FinancialTransactionItem> Items { get; set; } = new();

    public void UpdateTotal()
    {
        if (!Items.Any()) return;
        Total = Items.Sum(x => x.Amount);
    }
}

public class EntryFinancialTransaction : BaseFinancialTransaction
{
    public Guid EntryId { get; set; }
    public BaseEntry Entry { get; set; } = null!;
}

// public class EntryFinancialTransaction : BaseFinancialTransaction
// {
//     public Guid EntryId { get; set; }
//     public BaseEntry Entry { get; set; } = null!;
// }

public class AccountFinancialTransaction : BaseFinancialTransaction
{
    public Guid Account2Id { get; set; }
    public FinancialAccount Account2 { get; set; } = null!;

    /// <summary>
    /// От кого прилетело
    /// </summary>
    /// <returns></returns>
    public FinancialAccount From() => IsPlus ? Account2 : Account;
    
    /// <summary>
    /// Куда улетело
    /// </summary>
    /// <returns></returns>
    public FinancialAccount To() => IsPlus ? Account : Account2;
}