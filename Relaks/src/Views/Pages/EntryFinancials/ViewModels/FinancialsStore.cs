﻿using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Database.Repositories;
using Relaks.Models.FinancialModels;
using Relaks.src.Views.Pages.EntryFinancials;
using Size = BootstrapBlazor.Components.Size;

namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public partial class FinancialsStore(AppDbContext db, Guid entryId, DialogService dialogService)
{
    public Guid EntryId { get; } = entryId;
    public bool IsOpenAccounts { get; set; }

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
        OnFilterChanged();
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
    // private IQueryable<BaseFinancialTransaction> FindTransactionsQuery(Guid accountId) => db.BaseFinancialTransactions
    //     .Include(x => x.Account).ThenInclude(a => a.FinancialCurrency)
    //     .Include(x => x.Items).ThenInclude(item => item.Category)
    //     .Where(x => x.AccountId.Equals(accountId))
    //     .Where(x => x.CreatedAt >= FilterReq.From && x.CreatedAt < FilterReq.To);
    
    private void FindTransactions()
    {
        if (!AccountId.HasValue) return;
        Transactions = db.BaseFinancialTransactions
            .Include(x => x.Account).ThenInclude(a => a.FinancialCurrency)
            .Include(x => x.Items).ThenInclude(item => item.Category)
            .Where(x => x.AccountId.Equals(AccountId.Value))
            .Where(x => x.CreatedAt >= FilterReq.From && x.CreatedAt < FilterReq.To)
            .OrderByDescending(x => x.CreatedAt).ToList();
    }
    
    public Task ShowAccountFormModal(Guid? accountId = null)
    {
        IsOpenAccounts = false;
        var option = new DialogOption
        {
            IsKeyboard = true,
            IsBackdrop = true,
            Title = accountId.HasValue ? "Изменить счёт" : "Добавить счёт",
            IsScrolling = false,
            CloseButtonText = "Закрыть",
            Size = BootstrapBlazor.Components.Size.Large,
        };
        
        option.Component = BootstrapDynamicComponent.CreateComponent<_AccountForm>(new Dictionary<string, object?>()
        {
            ["EntryId"] = EntryId,
            ["AccountId"] = accountId,
            ["OnFormSubmit"] = EventCallback.Factory.Create(this, async _ =>
            {
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
            IsScrolling = false,
            CloseButtonText = "Закрыть",
            Size = BootstrapBlazor.Components.Size.Large,
        };
        
        option.Component = BootstrapDynamicComponent.CreateComponent<_AccountCategoryForm>(new Dictionary<string, object?>()
        {
            ["EntryId"] = EntryId,
            ["AccountCategoryId"] = editAccountCategoryId,
            ["OnFormSubmit"] = EventCallback.Factory.Create(this, async _ =>
            {
                FindAccountCategories();
                await option.CloseDialogAsync();
            }),
        });
        
        return dialogService.Show(option);
    }
    // public Task ShowTransactionFormModal(Tuple<Type, Guid?> typeAndTransactionId)
    // {
    //     ArgumentNullException.ThrowIfNull(Account);
    //     var type = typeAndTransactionId.Item1;
    //     var transactionId = typeAndTransactionId.Item2;
    //     
    //     IsOpenAccounts = false;
    //     var option = new DialogOption
    //     {
    //         IsKeyboard = true,
    //         IsBackdrop = true,
    //         Title = transactionId.HasValue ? "Изменить транзакцию" : "Добавить транзакцию",
    //         IsScrolling = false,
    //         CloseButtonText = "Закрыть",
    //         Size = Size.Large
    //         // FullScreenSize = FullScreenSize.Always
    //     };
    //     
    //     var componentConfig = new Dictionary<string, object?>()
    //     {
    //         ["Account"] = Account,
    //         ["TransactionCategories"] = db.FinancialTransactionCategories.ToBaseTree(),
    //         ["TransactionId"] = transactionId,
    //         ["OnFormSubmit"] = EventCallback.Factory.Create(this, async _ =>
    //         {
    //             OnFilterChanged();
    //             await option.CloseDialogAsync();
    //         }),
    //     };
    //
    //     if (type == typeof(AccountFinancialTransaction))
    //     {
    //         componentConfig["AccountCategories"] = AccountCategories;
    //         componentConfig["FinancialAccountSummaryBalances"] = FinancialAccountSummaryBalances;
    //     }
    //     
    //     option.Component = type == typeof(EntryFinancialTransaction) 
    //         ? BootstrapDynamicComponent.CreateComponent<_TransactionEntryForm>(componentConfig) 
    //         : BootstrapDynamicComponent.CreateComponent<_TransactionAccountForm>(componentConfig);
    //     
    //     return dialogService.Show(option);
    // }
}