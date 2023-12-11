using Relaks.Interfaces;

namespace Relaks.Views.Pages.Financials.ViewModels;

public class FinancialTransactionCategoryRequest : IParentable
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Title { get; set; } = null!;
}