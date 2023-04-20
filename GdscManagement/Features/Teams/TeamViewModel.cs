using System.ComponentModel.DataAnnotations;
using GdscManagement.Common.Features.Teams.Models;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Features.Base;
using GdscManagement.Utilities.Attributes;
using MudBlazor;

namespace GdscManagement.Features.Teams;

public class TeamViewModel : Team, IViewModel
{
    [Display(Name = "Team lead")]
    [DisplayExtras(Required = true)]
    public User TeamLead { get; set; }

    [Display(Name = "Team name")]
    [DisplayExtras(Required = true)]
    public string Name { get; set; }

    [Display(Name = "Members count")]
    [DisplayExtras(Required = true, InputType = InputType.Number)]
    public new int MembersCount { get; set; }

    public override string ToString()
    {
        return Name;
    }
}