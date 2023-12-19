namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public class FinancialAccountCategoryRequest
{
    public string Title { get; set; } = null!;
    public Guid? EntryId { get; set; }
}