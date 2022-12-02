using System.ComponentModel.DataAnnotations;
using System.Reflection;
using GdscManagement.Features.Base;
using GdscManagement.Utilities.Attributes;

namespace GdscManagement.Utilities;

public static class DisplayHelper
{
    public static DisplayExtrasAttribute GetExtrasInfo(this PropertyInfo property)
    {
        var attribute = property.GetDeepAttribute<DisplayExtrasAttribute, IViewModel>();
        return attribute ?? new DisplayExtrasAttribute();
    }

    public static DisplayAttribute GetDisplayInfo(this PropertyInfo property)
    {
        var attribute = property.GetDeepAttribute<DisplayAttribute, IViewModel>();
        return attribute ?? new DisplayAttribute{Name = property.Name};
    }

    private static T? GetDeepAttribute<T, I>(this MemberInfo property) where T : Attribute
    {
        var attribute = property.GetCustomAttribute<T>(true);
        var viewModelInterface = property.ReflectedType?.GetInterface(typeof(I).Name);
        var attributeFromInterface = viewModelInterface?.GetProperty(property.Name)?.GetCustomAttribute<T>();
        return attribute ?? attributeFromInterface;
    }
}
