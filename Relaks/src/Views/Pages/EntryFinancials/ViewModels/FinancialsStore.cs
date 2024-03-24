using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Database.Repositories;
using Relaks.Models.FinancialModels;
using Relaks.Models.Misc;
using Relaks.src.Views.Pages.Countries;
using Relaks.src.Views.Pages.EntryFinancials;

namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public partial class FinancialsStore(AppDbContext db, Guid entryId, DialogService dialogService)
{
    public Guid EntryId { get; } = entryId;
    public bool IsOpenAccounts { get; set; }

    // public enum SidebarEnum
    // {
    //     Default,
    //     AddAccount,
    //     EditAccount,
    //     AddAccountCategory,
    //     EditAccountCategory,
    // }
    //
    // public enum BodyEnum
    // {
    //     Default,
    //     AddTransactionCategory,
    //     EditTransactionCategory,
    //     AddAccountTransaction,
    //     EditAccountTransaction,
    //     AddEntryTransaction,
    //     EditEntryTransaction,
    //     TransactionCategories,
    // }

    // public BodyEnum BodyState { get; set; } = BodyEnum.Default;
    // public SidebarEnum SidebarState { get; set; } = SidebarEnum.Default;
    // public Guid? BodyEditTransactionId { get; set; }
    // public Guid? EditAccountCategoryId { get; set; }
    // public Guid? SidebarEditAccountId { get; set; }
    public List<FinancialCurrency> Currencies { get; set; } = new();
    public List<FinancialAccountCategory> AccountCategories { get; set; } = new();
    public FinancialAccountSummaryBalances FinancialAccountSummaryBalances { get; set; } = null!;
    
    public Guid? AccountId { get; set; }
    public FinancialAccount? Account { get; set; }
    public List<FinancialAccount> FinancialAccounts { get; set; } = new();
    public List<BaseFinancialTransaction> Transactions { get; set; } = new();

    /// <summary>
    /// Общие данные для всех счетов объединения
    /// </summary>
    public void Initialize()
    {
        FindCurrencies();
    }

    /// <summary>
    /// Действия при смене счёта
    /// </summary>
    public void OnChangeAccount()
    {
        FindAccountCategories();
        FindAccount();
        InitializeChartFilter();
        FindTransactions();
        Calculate();
    }

    private void FindCurrencies()
    {
        Currencies = db.FinancialCurrencies
            .OrderByDescending(x => x.Id.Equals("RUB"))
            .ThenByDescending(x => x.Id.Equals("USD"))
            .ThenByDescending(x => x.Id.Equals("EUR"))
            .ToList();
    }

    public void FindAccountCategories()
    {
        AccountCategories = db
            .FinancialAccountCategories
            .Where(x => x.EntryId.Equals(EntryId))
            .Include(x => x.Accounts).ThenInclude(x => x.FinancialCurrency)
            .OrderBy(x => x.Title)
            .ToList();

        FinancialAccounts = AccountCategories.SelectMany(x => x.Accounts).ToList();
        
        FinancialAccountSummaryBalances = new FinancialAccountSummaryBalances(AccountCategories);
        FinancialAccountSummaryBalances.Calculate();
    }

    public void FindAccount()
    {
        if (AccountId.HasValue)
        {
            Account = FinancialAccounts.FirstOrDefault(x => x.Id.Equals(AccountId.Value));
        }
        // else if (FinancialAccounts.Any())
        // {
        //     var first = FinancialAccounts.First();
        //     AccountId = first.Id;
        //     Account = first;
        // }
    }

    // public List<SelectedItem> AccountCategoriesSelectOptions() => db.FinancialAccountCategories
    //     .Where(x => x.EntryId.Equals(EntryId))
    //     .Select(x => new SelectedItem(x.Id.ToString(), x.Title))
    //     .ToList();
    //
    // public List<SelectedItem> CurrenciesSelectOptions() =>
    //     Currencies
    //         .Select(x => new SelectedItem(x.Id, x.ToString()))
    //         .ToList();
    
    // private void HandleFormSubmit()
    // {
    //     // FindCategories();
    //     // EditAccountCategoryId = null;
    // }

    // private Task OnCloseAsync()
    // {
    //     // EditAccountCategoryId = null;
    //     return Task.CompletedTask;
    // }
    private IQueryable<BaseFinancialTransaction> FindTransactionsQuery(Guid accountId) => db.BaseFinancialTransactions
        .Include(x => x.Account).ThenInclude(a => a.FinancialCurrency)
        .Include(x => x.Items).ThenInclude(item => item.Category)
        .Where(x => x.AccountId.Equals(accountId))
        .Where(x => x.CreatedAt >= FilterReq.From && x.CreatedAt < FilterReq.To);
    
    public void FindTransactions()
    {
        if (!AccountId.HasValue) return;
        var q = FindTransactionsQuery(AccountId.Value);
        Transactions = q.OrderByDescending(x => x.CreatedAt).ToList();
    }
    
    public Task ShowAccountFormModal(Guid? accountId = null)
    {
        IsOpenAccounts = false;
        var option = new DialogOption
        {
            IsKeyboard = true,
            IsBackdrop = true,
            Title = accountId.HasValue ? "Изменить счёт" : "Добавить счёт",
            IsScrolling = true,
            CloseButtonText = "Закрыть",
            Size = BootstrapBlazor.Components.Size.Large,
            // OnCloseAsync = OnCloseAsync
        };
        
        option.Component = BootstrapDynamicComponent.CreateComponent<_AccountForm>(new Dictionary<string, object?>()
        {
            ["EntryId"] = EntryId,
            ["AccountId"] = accountId,
            ["OnFormSubmit"] = EventCallback.Factory.Create(this, async _ =>
            {
                // HandleFormSubmit();
                FindAccountCategories();
                await option.CloseDialogAsync();
            }),
        });
        
        return dialogService.Show(option);
    }
    
    public Task ShowAccountCategoryFormModal(Guid? editAccountCategoryId = null)
    {
        IsOpenAccounts = false;
        var option = new DialogOption
        {
            IsKeyboard = true,
            IsBackdrop = true,
            Title = editAccountCategoryId.HasValue ? "Изменить категорию счёта" : "Добавить категорию счёта",
            IsScrolling = true,
            CloseButtonText = "Закрыть",
            Size = BootstrapBlazor.Components.Size.Large,
            // OnCloseAsync = OnCloseAsync
        };
        
        option.Component = BootstrapDynamicComponent.CreateComponent<_AccountCategoryForm>(new Dictionary<string, object?>()
        {
            ["EntryId"] = EntryId,
            ["AccountCategoryId"] = editAccountCategoryId,
            ["OnFormSubmit"] = EventCallback.Factory.Create(this, async _ =>
            {
                // HandleFormSubmit();
                FindAccountCategories();
                await option.CloseDialogAsync();
            }),
        });
        
        return dialogService.Show(option);
    }
}