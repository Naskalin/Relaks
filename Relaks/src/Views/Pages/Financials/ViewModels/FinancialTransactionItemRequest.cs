namespace Relaks.Views.Pages.Financials.ViewModels;

public class FinancialTransactionItemRequest
{
    public Guid? Id { get; set; }
    public Guid CategoryId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal Amount { get; set; } = 0.01m;
    public string? Description { get; set; }
}