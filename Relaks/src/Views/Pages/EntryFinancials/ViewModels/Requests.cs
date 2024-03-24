namespace Relaks.Views.Pages.EntryFinancials.ViewModels;

public class ChartFilterRequest
{
    public enum TypeEnum
    {
        AllByMonths,
        MonthByDays,
        YearByMonths,
        CustomByDays,
        CustomByMonths,
    }
    
    public TypeEnum Type { get; set; }
    
    /// <summary>
    /// Период от
    /// </summary>
    public DateTime From { get; set; }
    
    /// <summary>
    /// Период до
    /// </summary>
    public DateTime To { get; set; }
    
    public bool IsTypeByDays() => new[]
    {
        TypeEnum.MonthByDays,
        TypeEnum.CustomByDays
    }.Contains(Type);
    
    public bool IsTypeByMonths() => new[]
    {
        TypeEnum.AllByMonths,
        TypeEnum.CustomByMonths,
        TypeEnum.YearByMonths
    }.Contains(Type);
}