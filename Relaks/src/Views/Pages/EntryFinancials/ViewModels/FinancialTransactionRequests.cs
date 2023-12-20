using Relaks.Interfaces;

namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public class FinancialTransactionRequest
{
    public bool? IsPlus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? Description { get; set; }
    public Guid AccountId { get; set; }
    public Guid? EntryId { get; set; }
    public List<FinancialTransactionItemRequest> Items { get; set; } = new();
}

public class FinancialTransactionItemRequest
{
    public Guid? Id { get; set; }
    public Guid CategoryId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal Amount { get; set; } = 0.01m;
    public string? Description { get; set; }
}
