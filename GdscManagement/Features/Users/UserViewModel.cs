using System.ComponentModel.DataAnnotations;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Features.Base;
using GdscManagement.Utilities.Attributes;
using MudBlazor;

namespace GdscManagement.Features.Users;

public class UserViewModel : User, IViewModel
{
    [DisplayExtras(Required = true)]
    public override string? UserName { get; set; }

    [DisplayExtras(Required = true, InputType = InputType.Email)]
    public override string? Email { get; set; }

    [Display(Name = "First name", Description = "Your first name (ex. John)")]
    [DisplayExtras(Color = Color.Info, Required = true)]
    public override string? FirstName { get; set; }

    [Display(Name ="Last name")]
    [DisplayExtras(Required = true)]
    public override string? LastName { get; set; }

    [DisplayExtras(IsImage = true, InputType = InputType.Url)]
    public override string? Avatar { get; set; }

    [DisplayExtras(ReadOnly = true)]
    public override int AccessFailedCount { get; set; }

    [Hidden]
    public override string? NormalizedUserName { get; set; }
    [Hidden]
    public override string? NormalizedEmail { get; set; }
    [Hidden]
    public override string? PasswordHash { get; set; }
    [Hidden]
    public override string? SecurityStamp { get; set; }
    [Hidden]
    public override string? ConcurrencyStamp { get; set; }
    [Hidden]
    public override DateTimeOffset? LockoutEnd { get; set; }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}
