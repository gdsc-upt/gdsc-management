using System.ComponentModel.DataAnnotations;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Features.Base;
using GdscManagement.Utilities.Attributes;
using MudBlazor;

namespace GdscManagement.Features.Users;

public class UserViewModel : User, IViewModel
{
    public override string UserName { get; set; }

    [Display(Name = "First name", Description = "Your first name (ex. John)")]
    [DisplayExtras(Color = Color.Info, Required = true)]
    public override string? FirstName { get; set; }

    [Display(Name ="Last Name")]
    [DisplayExtras(Required = true)]
    public override string? LastName { get; set; }

    public override string? Avatar { get; set; }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}
