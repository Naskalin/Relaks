using System.ComponentModel.DataAnnotations;

namespace Relaks.Models.FinancialModels;

public class FinancialCurrency
{
    /// <summary>
    /// Трёх буквенный код валюты по ISO 4217
    /// </summary>
    [Key]
    public string Id { get; set; } = null!;

    /// <summary>
    /// Название валюты
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Символ валюты
    /// </summary>
    public string Symbol { get; set; } = null!;

    public override string ToString() => $"{Id} - {Title}, {Symbol}";
}