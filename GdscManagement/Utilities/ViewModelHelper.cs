using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using GdscManagement.Features.Base;
using GdscManagement.Utilities.Attributes;

namespace GdscManagement.Utilities;

public class ViewModelHelper<T> where T : IViewModel
{
    public string GetLabel(Expression<Func<T, object?>> property)
    {
        var member = GetMemberExpression(property);
        if (member is null)
        {
            return string.Empty;
        }

        var display = member.Member.GetCustomAttribute<DisplayAttribute>();
        return display?.Name ?? member.Member.Name;
    }

    public bool IsReadOnly(Expression<Func<T, object?>> property)
    {
        var member = GetMemberExpression(property);
        if (member is null)
        {
            return false;
        }

        var attribute = member.Member.GetCustomAttribute<DisplayExtrasAttribute>();
        return attribute?.ReadOnly ?? false;
    }

    private static MemberExpression? GetMemberExpression(Expression<Func<T, object?>> expr)
    {
        switch (expr.Body.NodeType)
        {
            case ExpressionType.Convert:
            case ExpressionType.ConvertChecked:
                var ue = expr.Body as UnaryExpression;
                return ue?.Operand as MemberExpression;
            default:
                return expr.Body as MemberExpression;
        }
    }
}
