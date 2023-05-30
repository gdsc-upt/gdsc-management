using GdscManagement.API.Features.Users.Models;
using GdscManagement.Common.Features.Projects.Models;

namespace GdscManagement.API.Features.Projects.Models;

public class ProjectResponse
{
    public string? Title { get; set; }

    public string? ManagerId { get; set; } 
    
    public ProjectStatuses Status { get; set; } // 0 1 sau 2
    
    public string? Client { get; set; }

    public List<UserResponse> Developers { get; set; } 
    
    public string Id { get; set; } 
}