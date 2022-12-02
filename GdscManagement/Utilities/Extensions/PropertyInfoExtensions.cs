using System.ComponentModel.DataAnnotations;
using System.Reflection;
using GdscManagement.Features.Base;
using GdscManagement.Utilities.Attributes;
using Humanizer;

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
        return attribute ?? new DisplayAttribute{Name = property.Name.SplitCamelCase().FirstToUpper()};
    }

    private static T? GetDeepAttribute<T, I>(this MemberInfo property) where T : Attribute
    {
        var attribute = property.GetCustomAttribute<T>(true);
        var viewModelInterface = property.ReflectedType?.GetInterface(typeof(I).Name);
        var attributeFromInterface = viewModelInterface?.GetProperty(property.Name)?.GetCustomAttribute<T>();
        return attribute ?? attributeFromInterface;
    }
}