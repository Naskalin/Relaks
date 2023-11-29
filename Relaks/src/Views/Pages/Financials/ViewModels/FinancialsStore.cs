using BootstrapBlazor.Components;
using Microsoft.EntityFrameworkCore;
using Relaks.Database;
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
    }

    public BodyEnum BodyState { get; set; } = BodyEnum.Default;
    public SidebarEnum SidebarState { get; set; } = SidebarEnum.Default;
    public Guid? BodyEditTransactionId { get; set; }
    public Guid? SidebarEditAccountCategoryId { get; set; }
    public Guid? SidebarEditAccountId { get; set; }
    public List<FinancialAccountCategory> AccountCategories { get; set; } = new();
    public List<FinancialTransactionCategory> TransactionCategories { get; set; } = new();
    private List<FinancialCurrency> Currencies { get; set; } = new();

    public void Initialize()
    {
        FindAccountCategories();
        FindCurrencies();
        FindTransactionCategories();
    }

    public void FindTransactionCategories()
    {
        TransactionCategories = db.FinancialTransactionCategories
            .OrderBy(x => x.Title)
            .ToList();
    }

    private void FindCurrencies()
    {
        Currencies = db.FinancialCurrencies
            .OrderByDescending(x => x.Id.Equals("RUB"))
            .ThenByDescending(x => x.Id.Equals("USD"))
            .ThenByDescending(x => x.Id.Equals("EUR"))
            .ToList();
    }
    
    private void FindAccountCategories()
    {
        AccountCategories = db.FinancialAccountCategories
            .OrderBy(x => x.Title)
            .Include(x => x.Accounts.OrderBy(a => a.Title))
            .ToList();
    }

    public List<SelectedItem> AccountSelectOptions()
    {
        var items = new List<SelectedItem>();
        AccountCategories.ForEach(category =>
        {
            foreach (var account in category.Accounts)
            {
                items.Add(new SelectedItem(account.Id.ToString(), account.TitleWithCurrency()) {GroupName = category.Title});
            }
        });
        
        return items;
    }

    public List<SelectedItem> AccountCategoriesSelectOptions() =>  AccountCategories
        .Select(x => new SelectedItem(x.Id.ToString(), x.Title))
        .ToList();

    public List<SelectedItem> CurrenciesSelectOptions() =>
        Currencies
            .Select(x => new SelectedItem(x.Id, x.ToString()))
            .ToList();
}