using System.ComponentModel.DataAnnotations;
using GdscManagement.Utilities.Attributes;

namespace GdscManagement.Features.Base;

public abstract class ViewModel
{
    [Display(Name = "Identifier")]
    [DisplayExtras(ReadOnly = true)]
    public string? Id { get; set; }

    [Display(Name = "Created")]
    [DisplayExtras(ReadOnly = true)]
    public DateTime Created { get; set; }

    [Display(Name = "Updated")]
    [DisplayExtras(ReadOnly = true)]
    public DateTime Updated { get; set; }
}

public interface IViewModel
{
    [Display(Name = "Identifier")]
    [DisplayExtras(ReadOnly = true)]
    public string Id { get; set; }

    [Display(Name = "Created")]
    [DisplayExtras(ReadOnly = true)]
    public DateTime Created { get; set; }

    [Display(Name = "Updated")]
    [DisplayExtras(ReadOnly = true)]
    public DateTime Updated { get; set; }
}
