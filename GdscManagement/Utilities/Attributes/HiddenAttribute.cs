namespace GdscManagement.Utilities.Attributes;

/**
 * <summary>
 * Use this attribute to hide a field or property from admin form
 * </summary>
 */
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class HiddenAttribute : Attribute
{
}
