using Relaks.Interfaces;

namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public class FinancialTransactionListRequest : IPaginatable
{
    public int Page { get; set; }
    public int PerPage { get; set; }
}