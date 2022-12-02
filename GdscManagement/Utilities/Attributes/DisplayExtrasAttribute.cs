using MudBlazor;

namespace GdscManagement.Utilities.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class DisplayExtrasAttribute : Attribute
{
    public Color Color { get; set; }
    /// <summary>
    ///     Makes the text field read only when editing a model
    ///     <para>
    ///         Also hides the text field when creating a new model
    ///     </para>
    /// </summary>
    /// <value>
    ///     Returns true if the text field is read only
    /// </value>
    public bool ReadOnly { get; set; }
    public bool Required { get; set; }
    public bool IsImage { get; set; }
    public bool HideOnTable { get; set; }
    public InputType InputType { get; set; } = InputType.Text;
}
