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

    private Tuple<DateTime, DateTime> GetPeriod()
    {
        var period = new Tuple<DateTime, DateTime>(Req.From, Req.To);
        if (Req.Type != FinancialAccountStatisticsRequest.TypeEnum.AllByMonths) return period;
        
        var transactionsQuery = db.BaseFinancialTransactions.Where(x => accountIds.Contains(x.AccountId));
        if (transactionsQuery.Any())
        {
            period = new Tuple<DateTime, DateTime>(
                transactionsQuery.Min(x => x.CreatedAt).StartOfMonth(),
                transactionsQuery.Max(x => x.CreatedAt).EndOfMonth()
            );
        }
        return period;
    }

    private void PeriodToDates(Tuple<DateTime, DateTime> period)
    {
        if (new[]
            {
                FinancialAccountStatisticsRequest.TypeEnum.YearByDays,
                FinancialAccountStatisticsRequest.TypeEnum.MonthByDays,
                FinancialAccountStatisticsRequest.TypeEnum.CustomByDays
            }.Contains(Req.Type))
        {
            Calculated.Dates = period.ToDays();
            return;
        }
        
        Calculated.Dates = period.ToMonths();
    }

    public void Calculate()
    {
        Calculated = new FinancialAccountLineChartModel();
        var period = GetPeriod();
        PeriodToDates(period);

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
                    .Where(x => x.CreatedAt >= period.Item1 && x.CreatedAt < period.Item2)
                    .OrderBy(x => x.CreatedAt)
                ;

            var accountModel = new FinancialAccountChartModel();
            var account = accounts[accountId];
            accountModel.Title = account.TitleWithCurrency();

            if (query.Any())
            {
                accountModel.TotalIncome = (decimal) query.Where(x => x.IsPlus == true).Sum(x => (double) x.Total);
                accountModel.TotalOutlay = - (decimal) query.Where(x => x.IsPlus == false).Sum(x => (double) x.Total);
                accountModel.Total = accountModel.TotalIncome + accountModel.TotalOutlay;
                accountModel.TransactionsCount = query.Count();
                // accountModel.AverageBalance = query.Last().Balance / accountModel.TransactionsCount;

                var items = query
                        .GroupBy(x => x.CreatedAt.Date)
                        .Select(g => new FinancialAccountChartItemModel
                        {
                            Date = g.Min(x => x.CreatedAt),
                            AverageBalance = (decimal) g.Average(x => (double) x.Balance),
                            TotalIncome = (decimal) g.Where(x => x.IsPlus).Sum(x => (double) x.Total),
                            TotalOutlay = - (decimal) g.Where(x => !x.IsPlus).Sum(x => (double) x.Total),
                            Total = (decimal) (g.Where(x => x.IsPlus).Sum(x => (double) x.Total) -
                                               g.Where(x => !x.IsPlus).Sum(x => (double) x.Total))
                        })
                        .OrderBy(x => x.Date)
                        .ToList()
                    ;

                // добавляем пустые данные
                var emptyDates = Calculated.Dates.Except(items.Select(x => x.Date.Date))
                    .OrderBy(x => x.Date)
                    .ToList();

                if (emptyDates.Any())
                {
                    foreach (var emptyDate in emptyDates)
                    {
                        //TODO: если это первая дата в диапозоне, то получается нуль это не правильно
                        // среднее значение есть в предыдущей записи в бд
                        // Вообще проверить как оно с пустыми данными работает
                        // в идеале наверное просто отдавать нет данных

                        // TODO: расставить сразу минусы для TotalOutlay и проставить их в коде
                        // var prevItem = items
                        //     .Where(x => emptyDate > x.Date)
                        //     .FirstOrDefault();
                        var prevItem = items.Where(x => emptyDate > x.Date).MaxBy(x => x.Date);
                        var averageBalance = prevItem?.AverageBalance;
                        
                        if (
                            // Если нет среднего
                            averageBalance == null
                            
                            // Если это самая первая пустая дата
                            && emptyDates.First() == emptyDate
                            
                            // и это первая дата совпадает с первой датой диапозона
                            && emptyDate.Date == period.Item1.Date)
                        {
                            averageBalance = db.BaseFinancialTransactions
                                .Where(x => accountIds.Contains(x.AccountId))
                                .Where(x => emptyDate > x.CreatedAt)
                                .OrderByDescending(x => x.CreatedAt)
                                .Select(x => x.Balance)
                                .FirstOrDefault();

                            if (!averageBalance.HasValue)
                            {
                                averageBalance = account.Balance;
                            }
                        }

                        items.Add(new FinancialAccountChartItemModel()
                        {
                            Date = emptyDate,
                            AverageBalance = averageBalance ?? 0,
                            TotalIncome = 0,
                            TotalOutlay = 0,
                            Total = 0,
                        });
                    }

                    items = items.OrderBy(x => x.Date).ToList();
                }

                accountModel.Items = items;
                accountModel.AverageBalance = accountModel.Items.Average(x => x.AverageBalance);
            }

            Calculated.Accounts.Add(accountModel);
        }
    }
}