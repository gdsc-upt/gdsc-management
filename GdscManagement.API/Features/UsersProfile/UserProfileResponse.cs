using GdscManagement.API.Features.Base;
using GdscManagement.Common.Features.Users.Models;

namespace GdscManagement.API.Features.UsersProfile.Models;

public class UserProfileResponse : ModelResponse
{
    public string UserId { get; set; }

    // TODO: add team field
    
    public string? FacebookLink { get; set; }

    public string? PhoneNumber { get; set; }

    public DateOnly Birthday { get; set; }
}