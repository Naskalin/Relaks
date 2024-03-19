﻿using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Models.FinancialModels;
using Relaks.src.Views.Pages.Countries;
using Relaks.src.Views.Pages.EntryFinancials;

namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public class FinancialsStore(AppDbContext db, Guid entryId, DialogService dialogService)
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
    public FinancialAccountStatistic FinancialAccountStatistic { get; set; } = null!;
    
    public Guid? AccountId { get; set; }
    public FinancialAccount? Account { get; set; }
    public List<FinancialAccount> FinancialAccounts { get; set; } = new();

    public void Initialize()
    {
        FindCurrencies();
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
        
        FinancialAccountStatistic = new FinancialAccountStatistic(AccountCategories);
        FinancialAccountStatistic.Calculate();
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