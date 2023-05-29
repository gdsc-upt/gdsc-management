using GdscManagement.Common.Features.Projects.Models;
using GdscManagement.Common.Features.Users.Models;

namespace GdscManagement.API.Features.Projects.Models;

public class DevelopersResponse
{
    public Project Project { get; set; }
    
    public User User { get; set; }
}