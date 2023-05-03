using GdscManagement.Common.Features.Base;
using GdscManagement.Common.Features.Teams.Models;
using GdscManagement.Common.Features.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace GdscManagement.Common.Features.UsersProfile.Models;

public class UserProfile : Model
{
    public User? User { get; set; }
    public Team? Team { get; set; }
    
    [PersonalData] public string? FacebookLink { get; set; }

    [PersonalData] public string? PhoneNumber { get; set; }

    [PersonalData] public DateOnly Birthday { get; set; }
}