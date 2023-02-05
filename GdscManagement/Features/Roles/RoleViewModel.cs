using System.ComponentModel.DataAnnotations;
using GdscManagement.Features.Base;
using GdscManagement.Utilities.Attributes;
using Microsoft.AspNetCore.Identity;

namespace GdscManagement.Features.Roles;

public class RoleViewModel: IdentityRole, IViewModel
{
    [Display(Name = "Identifier")]
    [DisplayExtras(ReadOnly = true, HideOnTable = true)]
    public override string? Id { get; set; }

    [Display(Name = "Created")]
    [DisplayExtras(ReadOnly = true, HideOnTable = true)]
    public DateTime Created { get; set; }

    [Display(Name = "Updated")]
    [DisplayExtras(ReadOnly = true)]
    public DateTime Updated { get; set; }
}
