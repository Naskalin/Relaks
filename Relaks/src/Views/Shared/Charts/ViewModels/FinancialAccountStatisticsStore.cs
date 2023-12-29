using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Utils.Extensions;

namespace Relaks.Views.Shared.Charts.ViewModels;

public class FinancialAccountStatisticsStore(AppDbContext db, List<Guid> accountIds)
{
    public FinancialAccountStatisticsRequest Req { get; set; } = new();

    public void Initialize()
    {
        var now = DateTime.Now;
        Req.TempDate = now;
        Req.To = now.EndDayOfMonth();
        Req.From = now.StartDayOfMonth();
    }
    
    public FinancialAccountLineChartModel Calculate()
    {
        var result = new FinancialAccountLineChartModel();

        result.Dates = Req.TempDate.ToMonthRangeDays();
        
        var accounts = db.FinancialAccounts
            .Where(x => accountIds.Contains(x.Id))
            .Include(x => x.FinancialCurrency)
            .ToDictionary(x => x.Id, x => x);
        
        foreach (var accountId in accountIds)
        {
            var query = db.BaseFinancialTransactions
                    .Where(x => x.AccountId.Equals(accountId))
                    .Where(x => x.CreatedAt >= Req.From && x.CreatedAt < Req.To)
                    .OrderBy(x => x.CreatedAt)
                ;

            var accountModel = new FinancialAccountChartModel();
            if (accounts.TryGetValue(accountId, out var account))
            {
                accountModel.Title = account.TitleWithCurrency();
            }

            if (query.Any())
            {
                accountModel.TotalIncome = (decimal) query.Where(x => x.IsPlus == true).Sum(x => (double) x.Total);
                accountModel.TotalOutlay = (decimal) query.Where(x => x.IsPlus == false).Sum(x => (double) x.Total);
                accountModel.Total = accountModel.TotalIncome - accountModel.TotalOutlay;
                accountModel.TransactionsCount = query.Count();
                accountModel.AverageBalance = query.Last().Balance / accountModel.TransactionsCount;

                var items = query
                        .GroupBy(x => x.CreatedAt.Date)
                        .Select(g => new FinancialAccountChartItemModel
                        {
                            Date = g.Min(x => x.CreatedAt),
                            AverageBalance = (decimal) g.Average(x => (double) x.Balance),
                            TotalIncome = (decimal) g.Where(x => x.IsPlus).Sum(x => (double) x.Total),
                            TotalOutlay = (decimal) g.Where(x => !x.IsPlus).Sum(x => (double) x.Total),
                            Total = (decimal) (g.Where(x => x.IsPlus).Sum(x => (double) x.Total) -
                                               g.Where(x => !x.IsPlus).Sum(x => (double) x.Total))
                        })
                        .OrderBy(x => x.Date)
                        .ToList()
                    ;

                accountModel.Items = items;
            }

            result.Accounts.Add(accountModel);
        }

        return result;
    }
}