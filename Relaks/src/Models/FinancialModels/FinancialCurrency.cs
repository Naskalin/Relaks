﻿using System.ComponentModel.DataAnnotations;

namespace Relaks.Models.FinancialModels;

public class FinancialCurrency
{
    /// <summary>
    /// Трёх буквенный код валюты по ISO 4217
    /// </summary>
    [Key]
    public required string Id { get; set; }
    
    /// <summary>
    /// Название валюты
    /// </summary>
    public required string Title { get; set; }
    
    /// <summary>
    /// Символ валюты
    /// </summary>
    public required string Symbol { get; set; }

    public override string ToString() => $"{Id} - {Title}, {Symbol}";
}