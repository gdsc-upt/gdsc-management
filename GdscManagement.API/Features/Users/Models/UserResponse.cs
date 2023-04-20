using GdscManagement.API.Features.Base;

namespace GdscManagement.API.Features.Users.Models;

public class UserResponse: ModelResponse
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string? Avatar { get; set; }
    public List<string> Roles { get; set; }
}
