using Relaks.Interfaces;

namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public abstract class BaseFinancialTransactionRequest
{
    public bool? IsPlus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? Description { get; set; }
    public Guid AccountId { get; set; }
    public List<FinancialTransactionItemRequest> Items { get; set; } = new();
}

public class EntryFinancialTransactionRequest : BaseFinancialTransactionRequest
{
    public Guid? EntryId { get; set; }
}

public class AccountFinancialTransactionRequest : BaseFinancialTransactionRequest
{
    public Guid? SecondAccountId;
}

public class FinancialTransactionItemRequest
{
    public Guid? Id { get; set; }
    public Guid? CategoryId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal Amount { get; set; } = 0.01m;
    public string? Description { get; set; }
}
