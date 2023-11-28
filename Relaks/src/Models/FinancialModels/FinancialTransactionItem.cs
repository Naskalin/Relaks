using System.ComponentModel.DataAnnotations.Schema;

namespace Relaks.Models.FinancialModels;

public class FinancialTransactionItem
{
    public Guid Id { get; set; }

    public Guid TransactionId { get; set; } = default!;
    public FinancialTransaction Transaction { get; set; } = null!;

    public Guid CategoryId { get; set; } = default!;
    public FinancialTransactionCategory Category { get; set; } = null!;

    public int Quantity { get; set; } = 1;
    
    /// <summary>
    /// Общая сумма за всё, за единицу высчитывается динамически
    /// </summary>
    [Column(TypeName = "money")]
    public decimal Amount { get; set; }
    
    public string? Description { get; set; }

    private decimal AmountSingle() => Amount / Quantity;
}