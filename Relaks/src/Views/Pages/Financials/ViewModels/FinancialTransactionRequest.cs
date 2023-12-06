using Relaks.Interfaces;

namespace Relaks.Views.Pages.Financials.ViewModels;

public class FinancialTransactionListRequest : IPaginatable
{
    public int Page { get; set; }
    public int PerPage { get; set; }
}

public class FinancialTransactionRequest
{
    public bool? IsPlus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? Description { get; set; }
    public Guid AccountId { get; set; }
    public Guid? EntryId { get; set; }
    public List<FinancialTransactionItemRequest> Items { get; set; } = new();
}

