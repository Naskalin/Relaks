﻿using Relaks.Interfaces;

namespace Relaks.Models.FinancialModels;

public class FinancialTransactionCategory : ITree<FinancialTransactionCategory>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<FinancialTransactionCategory> Children { get; set; } = new();
    public Guid? ParentId { get; set; }
    public FinancialTransactionCategory? Parent { get; set; }
    public string TreePath { get; set; } = null!;
    public required string Title { get; set; }
}