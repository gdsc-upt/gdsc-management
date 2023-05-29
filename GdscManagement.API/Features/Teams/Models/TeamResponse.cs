using GdscManagement.API.Features.Users.Models;
using GdscManagement.Common.Features.Users.Models;

namespace GdscManagement.API.Features.Teams.Models;

public class TeamResponse
{
    public User TeamLead { get; set; } 
    
    public string Name { get; set; }
    
    public int MembersCount { get; set; }
    
    public string Descriptions { get; set; }
}