namespace Relaks.Models.FinancialModels;

public class FinancialTransactionItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public required Guid TransactionId { get; set; }
    public required FinancialTransaction Transaction { get; set; }
    
    public required Guid CategoryId { get; set; }
    public required FinancialTransactionCategory Category { get; set; }
    
    public int Quantity { get; set; }
    public string? Description { get; set; }
}