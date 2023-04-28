using GdscManagement.Common.Features.Base;
using GdscManagement.Common.Features.Users.Models;

namespace GdscManagement.Common.Features.Teams.Models;

public class Team : Model
{
    public string TeamLeadId { get; set; }
    
    public string Name { get; set; }
    
    public int MembersCount { get; set; }
}


