using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Relaks.Interfaces;

namespace Relaks.Utils.Extensions;

public static class OrderByStringExtension
{
    public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, IOrderable orderable)
    {
        if (string.IsNullOrEmpty(orderable.OrderBy))
            throw new ArgumentNullException(orderable.OrderBy, "orderBy is empty");  
        
        string command = orderable.IsOrderByDesc is true ? "OrderByDescending" : "OrderBy";
        var type = typeof(TEntity);
        var pascalCaseProperty = PascalCase(orderable.OrderBy);
        var property = type.GetProperty(pascalCaseProperty);
        var parameter = Expression.Parameter(type, "p");
        if (property != null)
        {
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));
            return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        throw new ArgumentNullException(pascalCaseProperty, "Property for OrderBy filter not found");  
    }
    
    static string CamelCase(string s)
    {
        var x = s.Replace("_", "");
        if (x.Length == 0) return "null";
        x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])",
            m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
        return char.ToLower(x[0]) + x.Substring(1);
    }
    
    static string PascalCase(string s)
    {
        var x = CamelCase(s);
        return char.ToUpper(x[0]) + x.Substring(1);
    }
}