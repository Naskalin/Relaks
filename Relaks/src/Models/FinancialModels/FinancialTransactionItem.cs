using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Relaks.Models.FinancialModels;

public class FinancialTransactionItem
{
    public Guid Id { get; set; }

    public Guid TransactionId { get; set; }
    public BaseFinancialTransaction Transaction { get; set; } = null!;

    public Guid CategoryId { get; set; }
    public FinancialTransactionCategory Category { get; set; } = null!;

    public int Quantity { get; set; } = 1;
    
    /// <summary>
    /// Общая сумма за всё, за единицу высчитывается динамически
    /// </summary>
    [Precision(19, 4)]
    public decimal Amount { get; set; }
    
    [MaxLength(500)]
    public string? Description { get; set; }

    private decimal AmountSingle() => Amount / Quantity;
}