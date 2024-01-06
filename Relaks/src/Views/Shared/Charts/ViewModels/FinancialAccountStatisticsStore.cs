using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Utils.Extensions;
using DateTime = System.DateTime;

namespace Relaks.Views.Shared.Charts.ViewModels;

public class FinancialAccountStatisticsStore(AppDbContext db, List<Guid> accountIds)
{
    public FinancialAccountStatisticsRequest Req { get; set; } = new();
    public FinancialAccountLineChartModel Calculated { get; set; } = new();

    public void Initialize()
    {
        var now = DateTime.Now;
        Req.To = now.EndOfMonth();
        Req.From = now.StartOfMonth();
    }
    
    public void Calculate()
    {
        Calculated = new FinancialAccountLineChartModel();
        var period = new Tuple<DateTime, DateTime>(Req.From, Req.To);
        if (new[]
            {
                FinancialAccountStatisticsRequest.TypeEnum.YearByDays,
                FinancialAccountStatisticsRequest.TypeEnum.MonthByDays,
                FinancialAccountStatisticsRequest.TypeEnum.CustomByDays,
            }.Contains(Req.Type))
        {
            Calculated.Dates = period.ToDays();
        }
        else
        {
            // months types
            Calculated.Dates = period.ToMonths();
        }
        
        var accounts = db.FinancialAccounts
            .Where(x => accountIds.Contains(x.Id))
            .Include(x => x.FinancialCurrency)
            .ToDictionary(x => x.Id, x => x);

        Calculated.CurrencyId = accounts.Values.FirstOrDefault()?.FinancialCurrencyId;
        Calculated.CurrencySymbol = accounts.Values.FirstOrDefault()?.FinancialCurrency.Symbol;
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

                // добавляем пустые данные
                var emptyDates = Calculated.Dates.Except(items.Select(x => x.Date.Date)).ToList();
                
                if (emptyDates.Any())
                {
                    foreach (var emptyDate in emptyDates)
                    {
                        //TODO: если это первая дата в диапозоне, то получается нуль это не правильно
                        // среднее значение есть в предыдущей записи в бд
                        // Вообще проверить как оно с пустыми данными работает
                        // в идеале наверное просто отдавать нет данных
                        
                        // TODO: расставить сразу минусы для TotalOutlay и проставить их в коде
                        var prevItem = items.FirstOrDefault(x => emptyDate > x.Date);
                    
                        items.Add(new FinancialAccountChartItemModel()
                        {
                            Date = emptyDate,
                            AverageBalance = prevItem?.AverageBalance ?? 0,
                            TotalIncome = 0,
                            TotalOutlay = 0,
                            Total = 0,
                        });
                    }

                    items = items.OrderBy(x => x.Date).ToList();
                }
                
                accountModel.Items = items;
            }

            Calculated.Accounts.Add(accountModel);
        }
    }
}