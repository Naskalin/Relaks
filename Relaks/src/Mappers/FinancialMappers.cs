using Relaks.Models.FinancialModels;
using Relaks.Views.Pages.Financials.ViewModels;

namespace Relaks.Mappers;

public static class FinancialMappers
{
    public static void MapTo(this FinancialAccountCategory category, FinancialAccountCategoryRequest req)
    {
        req.Title = category.Title;
    }
    
    public static void MapTo(this FinancialAccountCategoryRequest req, FinancialAccountCategory category)
    {
        category.Title = req.Title;
    }
}