using System.ComponentModel.DataAnnotations;
using System.Reflection;
using GdscManagement.Utilities.Attributes;

namespace GdscManagement.Utilities;

public static class DisplayHelper
{
    public static DisplayExtrasAttribute GetExtrasInfo(this PropertyInfo property)
    {
        var attribute = property.GetCustomAttribute<DisplayExtrasAttribute>();
        return attribute ?? new DisplayExtrasAttribute();
    }

    public static DisplayAttribute GetDisplayInfo(this PropertyInfo property)
    {
        var attribute = property.GetCustomAttribute<DisplayAttribute>();
        return attribute ?? new DisplayAttribute{Name = property.Name};
    }
}
