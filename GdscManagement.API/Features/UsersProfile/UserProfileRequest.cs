using System.ComponentModel.DataAnnotations;

namespace GdscManagement.API.Features.UsersProfile;

public class UserProfileRequest
{
    [Required] public string? UserId { get; set; }

    [Required] public string? TeamId { get; set; }

    [Required] [Url] public string? FacebookLink { get; set; }
    //TODO : might be nice to check if it has facebook in link

    [Required] [Phone] public string? PhoneNumber { get; set; }

    [Required] public DateOnly Birthday { get; set; }
}