using Relaks.Models.FinancialModels;

namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public class FinancialAccountSummaryBalances(IEnumerable<FinancialAccountCategory> financialAccountCategories)
{
    public class CurrencyBalance
    {
        public FinancialCurrency FinancialCurrency { get; set; } = null!;
        public decimal Balance { get; set; }
    }
    public class CategoryBalance : CurrencyBalance
    {
        public Guid AccountCategoryId { get; set; }
    }
    public class TotalBalance
    {
        public List<CurrencyBalance> CurrencyBalances { get; set; } = new();
        public List<CategoryBalance> CategoryBalances { get; set; } = new();
    }

    public TotalBalance Data { get; set; } = new();

    public void Calculate()
    {
        foreach (var category in financialAccountCategories)
        {
            foreach (var account in category.Accounts)
            {
                var currency = account.FinancialCurrency;
                var currencyBalance =
                    Data.CurrencyBalances.FirstOrDefault(x => x.FinancialCurrency.Id.Equals(currency.Id));
                if (currencyBalance == null)
                {
                    currencyBalance = new CurrencyBalance() { FinancialCurrency = currency, Balance = 0 };
                    Data.CurrencyBalances.Add(currencyBalance);
                }
                currencyBalance.Balance += account.Balance;

                var categoryBalance = Data.CategoryBalances.FirstOrDefault(
                    x => x.FinancialCurrency.Equals(currency)
                         && x.AccountCategoryId.Equals(category.Id)
                );
                if (categoryBalance == null)
                {
                    categoryBalance = new CategoryBalance()
                        { AccountCategoryId = category.Id, FinancialCurrency = currency, Balance = 0 };
                    Data.CategoryBalances.Add(categoryBalance);
                }

                categoryBalance.Balance += account.Balance;
            }
        }
    }
}