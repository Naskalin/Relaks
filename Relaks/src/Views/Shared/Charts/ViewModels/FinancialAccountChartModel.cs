namespace Relaks.Views.Shared.Charts.ViewModels;

public class FinancialAccountLineChartModel
{
    public List<DateTime> Dates { get; set; } = new(); 
    public List<FinancialAccountChartModel> Accounts { get; set; } = new();
}

public class FinancialAccountChartModel
{
    
    public decimal Total { get; set; }
    
    /// <summary>
    /// Пополнений за этот период
    /// </summary>
    public decimal TotalIncome { get; set; }
    
    /// <summary>
    /// Списаний за этот период
    /// </summary>
    public decimal TotalOutlay { get; set; }
    
    /// <summary>
    /// Средний баланс за этот период
    /// </summary>
    public decimal AverageBalance { get; set; }
    
    /// <summary>
    /// Кол-во транзакций
    /// </summary>
    public int TransactionsCount { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; } = "???";

    public List<FinancialAccountChartItemModel> Items { get; set; } = new();
}

public class FinancialAccountChartItemModel
{
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    /// <summary>
    /// Пополнений за этот период
    /// </summary>
    public decimal TotalIncome { get; set; }
    
    /// <summary>
    /// Списаний за этот период
    /// </summary>
    public decimal TotalOutlay { get; set; }
    public decimal AverageBalance { get; set; }
}