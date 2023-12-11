using Relaks.Interfaces;

namespace Relaks.Models.FinancialModels;

public class FinancialTransactionCategory : ITree<FinancialTransactionCategory>
{
    public Guid Id { get; set; }
    public List<FinancialTransactionCategory> Children { get; set; } = new();
    public Guid? ParentId { get; set; }
    public FinancialTransactionCategory? Parent { get; set; }
    public string TreePath { get; set; } = null!;
    public string Title { get; set; } = null!;
}