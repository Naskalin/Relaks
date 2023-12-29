using System.ComponentModel;

namespace Relaks.Views.Shared.Charts.ViewModels;

public class FinancialAccountStatisticsRequest
{
    public enum TypeEnum
    {
        MonthByTransactions,
        MonthByDays,
        YearByMonths,
        YearByDays,
        YearByTransactions,
        CustomByDays,
        CustomByMonths,
        CustomByTransactions,
    }
    
    public TypeEnum Type { get; set; } = TypeEnum.MonthByDays;
    
    public DateTime TempDate { get; set; }
    
    /// <summary>
    /// Период от
    /// </summary>
    public DateTime From { get; set; }
    
    /// <summary>
    /// Период до
    /// </summary>
    public DateTime To { get; set; }
}