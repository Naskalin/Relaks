using BootstrapBlazor.Components;
using Relaks.Database;
using Relaks.Database.Repositories;
using Relaks.Models.FinancialModels;

namespace Relaks.Views.Pages.Financials.ViewModels;

public class FinancialsStore(AppDbContext db)
{
    public enum SidebarEnum
    {
        Default,
        AddAccount,
        EditAccount,
        AddAccountCategory,
        EditAccountCategory,
    }

    public enum BodyEnum
    {
        Default,
        AddTransactionCategory,
        EditTransactionCategory,
        AddTransaction,
        EditTransaction,
        TransactionCategories,
    }

    public BodyEnum BodyState { get; set; } = BodyEnum.Default;
    public BodyEnum? BackToBodyState { get; set; }
    public SidebarEnum SidebarState { get; set; } = SidebarEnum.Default;
    public Guid? BodyEditTransactionId { get; set; }
    public Guid? BodyEditTransactionCategoryId { get; set; }
    public Guid? SidebarEditAccountCategoryId { get; set; }
    public Guid? SidebarEditAccountId { get; set; }
    public List<FinancialTransactionCategory> TransactionCategories { get; set; } = new();
    public List<FinancialCurrency> Currencies { get; set; } = new();
    public Guid? AccountId { get; set; }

    public void Initialize()
    {
        FindCurrencies();
        FindTransactionCategories();
    }

    public void FindTransactionCategories()
    {
        TransactionCategories = db.FinancialTransactionCategories.ToBaseTree();
    }
    
    private void FindCurrencies()
    {
        Currencies = db.FinancialCurrencies
            .OrderByDescending(x => x.Id.Equals("RUB"))
            .ThenByDescending(x => x.Id.Equals("USD"))
            .ThenByDescending(x => x.Id.Equals("EUR"))
            .ToList();
    }

    public List<SelectedItem> AccountCategoriesSelectOptions() => db.FinancialAccountCategories
        .Select(x => new SelectedItem(x.Id.ToString(), x.Title))
        .ToList();

    public List<SelectedItem> CurrenciesSelectOptions() =>
        Currencies
            .Select(x => new SelectedItem(x.Id, x.ToString()))
            .ToList();
}