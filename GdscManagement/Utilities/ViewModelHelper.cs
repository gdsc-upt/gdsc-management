using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using GdscManagement.Features.Base;
using MudBlazor;

namespace GdscManagement.Utilities;

public class ViewModelHelper<T> where T : ViewModel
{
    public string GetLabel(Expression<Func<T, object>> property)
    {
        var member = GetMemberExpression(property);
        if (member is null)
        {
            return string.Empty;
        }

        var display = member.Member.GetCustomAttribute<LabelAttribute>();
        return display?.Name ?? member.Member.Name;
    }

    public bool IsReadOnly(Expression<Func<T, object>> property)
    {
        var member = GetMemberExpression(property);
        if (member is null)
        {
            return false;
        }

        var attribute = member.Member.GetCustomAttribute<ReadOnlyAttribute>();
        return attribute?.IsReadOnly ?? false;
    }

    private static MemberExpression? GetMemberExpression(Expression<Func<T, object>> expr)
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
