using System.ComponentModel.DataAnnotations;
using GdscManagement.Features.Base;
using GdscManagement.Utilities.Attributes;
using MudBlazor;

namespace GdscManagement.Features.Users;

public class UserViewModel : ViewModel
{
    [Display(Name = "First name", Description = "Your first name (ex. John)")]
    [DisplayExtras(Color = Color.Info, Required = true)]
    public string? FirstName { get; set; }

    [Display(Name ="Last Name")]
    [DisplayExtras(Required = true)]
    public string? LastName { get; set; }

    public string? Avatar { get; set; }
    public DateTime? NullableDateTime { get; set; }
    public DateTime DateTime { get; set; }
    public bool Boolean { get; set; }
    public int Integer { get; set; }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}
