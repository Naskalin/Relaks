using System.ComponentModel.DataAnnotations;

namespace Relaks.Models.FinancialModels;

public class FinancialCurrency
{
    /// <summary>
    /// Трёх буквенный код валюты по ISO 4217
    /// </summary>
    [Key]
    [MaxLength(3)]
    public string Id { get; set; } = null!;

    /// <summary>
    /// Название валюты
    /// </summary>
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Символ валюты
    /// </summary>
    [MaxLength(20)]
    public string Symbol { get; set; } = null!;

    public override string ToString() => $"{Id} - {Title}, {Symbol}";
}