using Microsoft.EntityFrameworkCore;
using Relaks.Database;
using Relaks.Utils.Extensions;

namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public partial class FinancialsStore
{
    public ChartFilterRequest FilterReq { get; set; } = null!;
    public ChartLineModel AccountStatistic { get; set; } = new();

    private void InitializeChartFilter()
    {
        var now = DateTime.Now;
        FilterReq = new ChartFilterRequest()
        {
            To = now.EndOfDay(),
            From = now.AddDays(-30).StartOfDay(),
            Type = ChartFilterRequest.TypeEnum.CustomByDays
        };
    }

    public void OnFilterChanged()
    {
        FindTransactions();
        Calculate();
    }

    // private Tuple<DateTime, DateTime> GetPeriod()
    // {
    //     var period = new Tuple<DateTime, DateTime>(FilterReq.From, FilterReq.To);
    //     if (FilterReq.Type != ChartFilterRequest.TypeEnum.AllByMonths) return period;
    //     
    //     var transactionsQuery = db.BaseFinancialTransactions.Where(x => accountIds.Contains(x.AccountId));
    //     if (transactionsQuery.Any())
    //     {
    //         period = new Tuple<DateTime, DateTime>(
    //             transactionsQuery.Min(x => x.CreatedAt).StartOfMonth(),
    //             transactionsQuery.Max(x => x.CreatedAt).EndOfMonth()
    //         );
    //     }
    //     return period;
    // }
    //
    public void Calculate()
    {
        if (Account == null) return;
        var period = new Tuple<DateTime, DateTime>(FilterReq.From, FilterReq.To);
        AccountStatistic = new ChartLineModel()
        {
            Title = Account.TitleWithCurrency(),
            CurrencyId = Account.FinancialCurrencyId,
            CurrencySymbol = Account.FinancialCurrency.Symbol,
            Dates = FilterReq.IsTypeByDays() ? period.ToDays() : period.ToMonths(),
        };

        var q = FindTransactionsQuery(Account.Id).OrderBy(x => x.CreatedAt);

        if (q.Any())
        {
            AccountStatistic.TotalIncome = (decimal)q.Where(x => x.IsPlus == true).Sum(x => (double)x.Total);
            AccountStatistic.TotalOutlay = -(decimal)q.Where(x => x.IsPlus == false).Sum(x => (double)x.Total);
            AccountStatistic.BalanceChanges = AccountStatistic.TotalIncome + AccountStatistic.TotalOutlay;
            AccountStatistic.PlusTransactionsCount = q.Count(x => x.IsPlus);
            AccountStatistic.MinusTransactionsCount = q.Count(x => !x.IsPlus);
            AccountStatistic.AverageBalance = (decimal) q.Average(x => (double) x.Balance);

            var items = q.GroupBy(x => x.CreatedAt.Date)
                    .Select(g => new ChartItemModel
                    {
                        Date = g.Min(x => x.CreatedAt),
                        AverageBalance = (decimal)g.Average(x => (double)x.Balance),
                        TotalIncome = (decimal)g.Where(x => x.IsPlus).Sum(x => (double)x.Total),
                        TotalOutlay = -(decimal)g.Where(x => !x.IsPlus).Sum(x => (double)x.Total),
                        BalanceChanges = (decimal)
                                g.Where(x => x.IsPlus).Sum(x => (double)x.Total)
                                -(decimal)g.Where(x => !x.IsPlus).Sum(x => (double)x.Total)
                    })
                    .OrderBy(x => x.Date)
                    .ToList()
                ;

            // добавляем пустые данные
            var emptyDates = AccountStatistic.Dates.Except(items.Select(x => x.Date.Date))
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
                        var transactionQuery = db.BaseFinancialTransactions.Where(x => x.AccountId.Equals(Account.Id));

                        var dbPrevItem = transactionQuery
                            .Where(x => emptyDate > x.CreatedAt)
                            .OrderByDescending(x => x.CreatedAt)
                            .FirstOrDefault();

                        averageBalance = dbPrevItem?.Balance;

                        if (!averageBalance.HasValue)
                        {
                            // Предшествующих данной транзакции тоже нет
                            // значит берём начальный баланс из самой первой транзакции
                            var firstTransaction = transactionQuery
                                .OrderBy(x => x.CreatedAt)
                                .FirstOrDefault();
                            averageBalance = firstTransaction?.FromBalance();
                        }
                    }

                    items.Add(new ChartItemModel()
                    {
                        Date = emptyDate,
                        AverageBalance = averageBalance ?? 0,
                        TotalIncome = 0,
                        TotalOutlay = 0,
                        BalanceChanges = 0,
                    });
                }

                items = items.OrderBy(x => x.Date).ToList();
            }

            AccountStatistic.Items = items;
            AccountStatistic.AverageBalance = items.Average(x => x.AverageBalance);
        }
    }
}