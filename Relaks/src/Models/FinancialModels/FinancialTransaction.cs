using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Relaks.Models.FinancialModels;

[Table("FinancialTransactions")]
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

    [MaxLength(50)]
    public string Discriminator { get; set; } = null!;

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
    
    public List<FinancialTransactionItem> Items { get; set; } = new();
    
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

public class AccountFinancialTransaction : BaseFinancialTransaction
{
    public Guid SecondAccountId { get; set; }
    public FinancialAccount SecondAccount { get; set; } = null!;
    
    public Guid ReverseTransactionId { get; set; }
    public AccountFinancialTransaction ReverseTransaction { get; set; } = null!;
}