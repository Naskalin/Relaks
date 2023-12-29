using Relaks.Models.FinancialModels;
using Relaks.Views.Shared.Charts.ViewModels;

namespace Relaks.Mappers;

public static class FinancialStatisticMappers
{
    // public static void MapTo(this IQueryable<BaseFinancialTransaction> query, FinancialAccountChartModel model)
    // {
    //     var total = query.Where(x => x.IsPlus == true).Sum(x => (double) x.Total) -
    //                 query.Where(x => x.IsPlus == false).Sum(x => (double) x.Total);
    //     model.Total = (decimal) total;
    //     model.TransactionsCount = query.Count();
    //     model.AverageBalance = query.Last().Balance / model.TransactionsCount;    
    // }
    
    // public static FinancialAccountChartItemModel ToItem(this IGrouping<DateTime, BaseFinancialTransaction> group)
    // {
    //     return new FinancialAccountChartItemModel
    //     {
    //         Date = group.Max(x => x.CreatedAt),
    //         AverageBalance = (decimal) group.Average(x => (double) x.Balance),
    //         Total = (decimal) (group.Where(x => x.IsPlus).Sum(x => (double) x.Total) -
    //                            group.Where(x => !x.IsPlus).Sum(x => (double) x.Total)),
    //
    //     };
    // }
}