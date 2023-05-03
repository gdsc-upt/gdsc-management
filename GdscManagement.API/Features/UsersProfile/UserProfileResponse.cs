using GdscManagement.API.Features.Base;
using GdscManagement.Common.Features.Teams.Models;
using GdscManagement.Common.Features.Users.Models;

namespace GdscManagement.API.Features.UsersProfile;

public class UserProfileResponse : ModelResponse
{
    public User? User { get; set; }

    public Team? Team { get; set; }
    
    public string? FacebookLink { get; set; }

    public string? PhoneNumber { get; set; }

    public DateOnly Birthday { get; set; }
}