using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Mappers;

namespace Relaks.Views.Shared.Charts.ViewModels;

public class FinancialAccountStatisticsStore(AppDbContext db, List<Guid> accountIds)
{
    public FinancialAccountLineChartModel Calculate()
    {
        var data = new FinancialAccountLineChartModel();
        var accounts = db.FinancialAccounts
            .Where(x => accountIds.Contains(x.Id))
            .Include(x => x.FinancialCurrency)
            .ToDictionary(x => x.Id, x => x);

        foreach (var accountId in accountIds)
        {
            var query = db.BaseFinancialTransactions
                .Where(x => x.AccountId.Equals(accountId))
                .OrderBy(x => x.CreatedAt)
                ;
        
            var accountModel = new FinancialAccountChartModel();
            if (accounts.TryGetValue(accountId, out var account))
            {
                accountModel.Title = account.TitleWithCurrency();
            }
            if (query.Any())
            {
                query.MapTo(accountModel);
                // var total = query.Where(x => x.IsPlus == true).Sum(x => (double) x.Total) -
                            // query.Where(x => x.IsPlus == false).Sum(x => (double) x.Total);
                // accountModel.Total = (decimal) total;
                // accountModel.TransactionsCount = query.Count();
                // accountModel.AverageBalance = query.Last().Balance / accountModel.TransactionsCount;
                
                var items = query
                    .GroupBy(x => x.CreatedAt.Date)
                    .Select(g => new FinancialAccountChartItemModel
                    {
                        Date = g.Min(x => x.CreatedAt),
                        AverageBalance = (decimal) g.Average(x => (double) x.Balance),
                        Total = (decimal) (g.Where(x => x.IsPlus).Sum(x => (double) x.Total) -
                                           g.Where(x => !x.IsPlus).Sum(x => (double) x.Total)),
                    })
                    .OrderBy(x => x.Date)
                    .ToList()
                    ;
                
                accountModel.Items = items;
            }
            
            data.Accounts.Add(accountModel);
        }
        
        return data;
    }
}