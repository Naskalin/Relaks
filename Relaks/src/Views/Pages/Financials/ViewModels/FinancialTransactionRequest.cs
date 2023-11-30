namespace Relaks.Views.Pages.Financials.ViewModels;

public class FinancialTransactionRequest
{
    public bool? IsPlus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? Description { get; set; }
    public Guid AccountId { get; set; } = default!;
    public Guid? EntryId { get; set; }
    public List<FinancialTransactionItemRequest> Items { get; set; } = new();
}