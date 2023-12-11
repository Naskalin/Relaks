using BootstrapBlazor.Components;
using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Database.Repositories;
using Relaks.Managers;
using Relaks.Models.FinancialModels;
using Relaks.Models.Misc;

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
    public List<FinancialTransactionCategory> TransactionCategories { get; set; } = new();
    public List<FinancialCurrency> Currencies { get; set; } = new();
    // public PaginatableResult<FinancialTransaction> Transactions { get; set; } = new();
    public Guid? AccountId { get; set; }
    // public FinancialTransactionListRequest TransactionListRequest { get; set; } = new() {Page = 1, PerPage = 10};

    public void Initialize()
    {
        FindCurrencies();
        FindTransactionCategories();
        // FindTransactions();
    }

    // public void FindForAccount()
    // {
    //     FindTransactions();
    // }

    // public void FindTransactions()
    // {
    //     var q = db.FinancialTransactions
    //         .Include(x => x.Entry)
    //         .Include(x => x.Account).ThenInclude(a => a.FinancialCurrency)
    //         .OrderByDescending(x => x.CreatedAt);
    //     
    //     Transactions = AccountId.HasValue 
    //         ? q.Where(x => x.AccountId.Equals(AccountId.Value)).ToPaginatedResult(TransactionListRequest) 
    //         : q.ToPaginatedResult(TransactionListRequest);
    // }

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

    public List<SelectedItem> FindTransactionCategoriesSelectOptions() =>
        TransactionCategories.ToTreeSelect().Select(x => new SelectedItem(x.Value.ToString(), x.Title)).ToList();

    public List<SelectedItem> AccountCategoriesSelectOptions() => db.FinancialAccountCategories
        .Select(x => new SelectedItem(x.Id.ToString(), x.Title))
        .ToList();

    public List<SelectedItem> CurrenciesSelectOptions() =>
        Currencies
            .Select(x => new SelectedItem(x.Id, x.ToString()))
            .ToList();
}