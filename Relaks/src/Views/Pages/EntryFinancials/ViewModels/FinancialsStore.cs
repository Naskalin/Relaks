using BootstrapBlazor.Components;
using Relaks.Database;
using Relaks.Models.FinancialModels;

namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public class FinancialsStore(AppDbContext db, Guid entryId)
{
    public Guid EntryId { get; } = entryId;

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
        AddAccountTransaction,
        EditAccountTransaction,
        AddEntryTransaction,
        EditEntryTransaction,
        TransactionCategories,
    }

    public BodyEnum BodyState { get; set; } = BodyEnum.Default;
    public SidebarEnum SidebarState { get; set; } = SidebarEnum.Default;
    public Guid? BodyEditTransactionId { get; set; }
    public Guid? SidebarEditAccountCategoryId { get; set; }
    public Guid? SidebarEditAccountId { get; set; }
    public List<FinancialCurrency> Currencies { get; set; } = new();
    public Guid? AccountId { get; set; }

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

    public List<SelectedItem> AccountCategoriesSelectOptions() => db.FinancialAccountCategories
        .Where(x => x.EntryId.Equals(EntryId))
        .Select(x => new SelectedItem(x.Id.ToString(), x.Title))
        .ToList();

    public List<SelectedItem> CurrenciesSelectOptions() =>
        Currencies
            .Select(x => new SelectedItem(x.Id, x.ToString()))
            .ToList();
}