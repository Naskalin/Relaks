using Microsoft.EntityFrameworkCore;
using Relaks.Utils.Extensions;

namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public partial class FinancialsStore
{
    public ChartFilterRequest FilterReq { get; set; } = null!;
    public ChartLineModel AccountStatistic { get; set; } = new();

    /// <summary>
    /// Нужно ли перезагрузить визуальное отображение графиков при изменении данных
    /// </summary>
    public bool IsNeedReloadLineCharts { get; set; }

    public bool IsNeedReloadCategoryCharts { get; set; }

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
        IsNeedReloadLineCharts = true;
        IsNeedReloadCategoryCharts = true;
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

    private void CalculateCategories()
    {
        AccountStatistic.CategoryItems = db.FinancialTransactionItems
                .Include(x => x.Transaction)
                .Include(x => x.Category)
                .Where(x => x.Transaction.AccountId.Equals(Account.Id))
                .Where(x => x.Transaction.CreatedAt >= FilterReq.From && x.Transaction.CreatedAt < FilterReq.To)
                .GroupBy(x => x.CategoryId)
                .Select(g => new ChartCategoryItem()
                {
                    Id = g.Key,
                    Title = g.First().Category.Title,
                    Count = g.Count() + g.Sum(x => x.Quantity),
                    TotalIncome = (decimal) g.Where(i => i.Transaction.IsPlus == true).Sum(a => (double) a.Amount),
                    TotalOutcome = (decimal) - g.Where(i => i.Transaction.IsPlus == false).Sum(a => (double) a.Amount),
                    TreePath = g.First().Category.TreePath,
                    IsNotUsedDirectly = false,
                })
                .OrderByDescending(x => x.Count)
                .ToList()
            ;

        // categoryId, count usages
        // var otherCategories = new Dictionary<Guid, int>();
        var otherCategories = new Dictionary<Guid, ChartCategoryItem>();
        AccountStatistic.CategoryItems.ForEach(x =>
        {
            if (string.IsNullOrEmpty(x.TreePath)) return;

            var categoryIds = x.TreePath
                    .Split("/")
                    .Where(str => !string.IsNullOrEmpty(str))
                    .Select(Guid.Parse)
                    .Where(guid => !x.Id.Equals(guid))
                    .ToList()
                ;

            categoryIds.ForEach(id =>
            {
                if (!otherCategories.ContainsKey(id))
                {
                    otherCategories.Add(id, new ChartCategoryItem());
                }

                otherCategories[id].Count += x.Count;
                otherCategories[id].TotalIncome += x.TotalIncome;
                otherCategories[id].TotalOutcome -= x.TotalOutcome;
            });
        });

        AccountStatistic.CategoryItems.AddRange(
            db.FinancialTransactionCategories
                .Where(x => otherCategories.Keys.Contains(x.Id))
                .Select(x => new ChartCategoryItem
                {
                    Id = x.Id,
                    Title = x.Title,
                    Count = otherCategories[x.Id].Count,
                    TotalIncome = otherCategories[x.Id].TotalIncome,
                    TotalOutcome = otherCategories[x.Id].TotalOutcome,
                    IsNotUsedDirectly = true,
                })
                .ToList()
        );

        var allCounts = AccountStatistic.CategoryItems.Sum(x => x.Count);
        AccountStatistic.CategoryItems.ForEach(x =>
        {
            x.Percent = (double) (100 * x.Count) / allCounts;
        });
        
        AccountStatistic.CategoryItems = AccountStatistic.CategoryItems.OrderByDescending(x => x.Percent).ToList();
    }

    private void Calculate()
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

        var q = db.BaseFinancialTransactions
            .Where(x => x.AccountId.Equals(Account.Id))
            .Where(x => x.CreatedAt >= FilterReq.From && x.CreatedAt < FilterReq.To)
            .OrderBy(x => x.CreatedAt);

        if (q.Any())
        {
            CalculateCategories();
            
            AccountStatistic.TotalIncome = (decimal)q.Where(x => x.IsPlus == true).Sum(x => (double)x.Total);
            AccountStatistic.TotalOutlay = -(decimal)q.Where(x => x.IsPlus == false).Sum(x => (double)x.Total);
            AccountStatistic.BalanceChanges = AccountStatistic.TotalIncome + AccountStatistic.TotalOutlay;
            AccountStatistic.PlusTransactionsCount = q.Count(x => x.IsPlus);
            AccountStatistic.MinusTransactionsCount = q.Count(x => !x.IsPlus);
            AccountStatistic.AverageBalance = (decimal)q.Average(x => (double)x.Balance);

            List<ChartItemModel> items;
            List<DateTime> emptyDates; // добавляем пустые даты

            if (FilterReq.IsTypeByDays())
            {
                items = q.GroupBy(x => x.CreatedAt.Date)
                        .Select(g => new ChartItemModel
                        {
                            Date = g.Min(x => x.CreatedAt),
                            AverageBalance = (decimal)g.Average(x => (double)x.Balance),
                            TotalIncome = (decimal)g.Where(x => x.IsPlus).Sum(x => (double)x.Total),
                            TotalOutlay = -(decimal)g.Where(x => !x.IsPlus).Sum(x => (double)x.Total),
                            BalanceChanges = (decimal)
                                             g.Where(x => x.IsPlus).Sum(x => (double)x.Total)
                                             - (decimal)g.Where(x => !x.IsPlus).Sum(x => (double)x.Total),
                        })
                        .OrderBy(x => x.Date)
                        .ToList()
                    ;

                var endOfDateBalances = q.GroupBy(x => x.CreatedAt.Date)
                        .Select(g => g.OrderByDescending(y => y.CreatedAt).First())
                        .ToDictionary(x => x.CreatedAt.Date, x => x.Balance)
                    ;

                foreach (var item in items)
                {
                    if (endOfDateBalances.TryGetValue(item.Date.Date, out var endOf))
                    {
                        item.EndOfDateBalance = endOf;
                    }
                }

                emptyDates = AccountStatistic.Dates.Except(items.Select(x => x.Date.Date))
                    .OrderBy(x => x.Date)
                    .ToList();
            }
            else
            {
                items = q.GroupBy(x => new { x.CreatedAt.Year, x.CreatedAt.Month })
                        .Select(g => new ChartItemModel
                        {
                            Date = g.Min(x => x.CreatedAt),
                            AverageBalance = (decimal)g.Average(x => (double)x.Balance),
                            TotalIncome = (decimal)g.Where(x => x.IsPlus).Sum(x => (double)x.Total),
                            TotalOutlay = -(decimal)g.Where(x => !x.IsPlus).Sum(x => (double)x.Total),
                            BalanceChanges = (decimal)
                                             g.Where(x => x.IsPlus).Sum(x => (double)x.Total)
                                             - (decimal)g.Where(x => !x.IsPlus).Sum(x => (double)x.Total)
                        })
                        .AsEnumerable()
                        .OrderBy(x => x.Date)
                        .ToList()
                    ;

                var endOfDateBalances = q.GroupBy(x => new { x.CreatedAt.Year, x.CreatedAt.Month })
                        .Select(g => g.OrderByDescending(y => y.CreatedAt).First())
                        .ToDictionary(x => x.CreatedAt.Date.ToString("MM.yyyy"), x => x.Balance)
                    ;

                foreach (var item in items)
                {
                    if (endOfDateBalances.TryGetValue(item.Date.ToString("MM.yyyy"), out var endOf))
                    {
                        item.EndOfDateBalance = endOf;
                    }
                }

                emptyDates = AccountStatistic.Dates.Where(x =>
                        !items.Select(i => i.Date.Year).Contains(x.Year)
                        && !items.Select(i => i.Date.Month).Contains(x.Month)
                    )
                    .OrderBy(x => x.Date)
                    .ToList();
            }

            if (emptyDates.Any())
            {
                foreach (var emptyDate in emptyDates)
                {
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
                        EndOfDateBalance = prevItem?.EndOfDateBalance ?? 0,
                    });
                }

                items = items.OrderBy(x => x.Date).ToList();
            }

            AccountStatistic.Items = items;
            AccountStatistic.AverageBalance = items.Average(x => x.AverageBalance);
        }
    }
}