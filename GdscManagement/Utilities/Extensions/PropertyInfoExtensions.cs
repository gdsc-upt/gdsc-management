using System.ComponentModel.DataAnnotations;
using System.Reflection;
using GdscManagement.Features.Base;
using GdscManagement.Utilities.Attributes;

namespace GdscManagement.Utilities.Extensions;

public static class PropertyInfoExtensions
{
    public static bool IsHidden(this PropertyInfo property)
    {
        var attribute = property.GetDeepAttribute<HiddenAttribute, IViewModel>();
        return attribute is not null;
    }

    public static DisplayExtrasAttribute GetExtrasInfo(this PropertyInfo property)
    {
        var attribute = property.GetDeepAttribute<DisplayExtrasAttribute, IViewModel>();
        return attribute ?? new DisplayExtrasAttribute();
    }

    public static DisplayAttribute GetDisplayInfo(this PropertyInfo property)
    {
        var attribute = property.GetDeepAttribute<DisplayAttribute, IViewModel>();
        return attribute ?? new DisplayAttribute { Name = property.Name.SplitCamelCase().FirstToUpper() };
    }

    private static TAttribute? GetDeepAttribute<TAttribute, TInterface>(this MemberInfo property)
        where TAttribute : Attribute
    {
        var attribute = property.GetCustomAttribute<TAttribute>(true);
        var viewModelInterface = property.ReflectedType?.GetInterface(typeof(TInterface).Name);
        var attributeFromInterface = viewModelInterface?.GetProperty(property.Name)?.GetCustomAttribute<TAttribute>();
        return attribute ?? attributeFromInterface;
    }
}
