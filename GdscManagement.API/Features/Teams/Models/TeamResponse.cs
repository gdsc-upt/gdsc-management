using GdscManagement.API.Features.Users.Models;

namespace GdscManagement.API.Features.Teams.Models;

public class TeamResponse
{
    public string TeamLeadId { get; set; }
    
    public string Name { get; set; }
    
    public int MembersCount { get; set; }
}