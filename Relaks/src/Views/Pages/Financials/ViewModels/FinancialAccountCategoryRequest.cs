namespace Relaks.Views.Pages.Financials.ViewModels;

public class FinancialAccountCategoryRequest
{
    public string Title { get; set; } = null!;
    public Guid? EntryId { get; set; }
}