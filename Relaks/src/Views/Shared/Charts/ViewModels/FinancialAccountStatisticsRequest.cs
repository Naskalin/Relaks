using System.ComponentModel;

namespace Relaks.Views.Shared.Charts.ViewModels;

public class FinancialAccountStatisticsRequest
{
    public enum TypeEnum
    {
        AllByMonths,
        MonthByDays,
        YearByMonths,
        YearByDays,
        CustomByDays,
        CustomByMonths,
    }
    
    public TypeEnum Type { get; set; } = TypeEnum.MonthByDays;
    
    /// <summary>
    /// Период от
    /// </summary>
    public DateTime From { get; set; }
    
    /// <summary>
    /// Период до
    /// </summary>
    public DateTime To { get; set; }
}