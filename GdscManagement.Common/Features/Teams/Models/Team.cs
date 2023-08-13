using GdscManagement.Common.Features.Base;
using GdscManagement.Common.Features.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace GdscManagement.Common.Features.Teams.Models;

public class Team : Model
{
    public User TeamLead { get; set; }
    
    public string Name { get; set; }
    
    public int MembersCount { get; set; }
    
    public string Descriptions { get; set; }
   

    public static async Task<IdentityResult?> CreateAsync(Team map)
    {
        throw new NotImplementedException();
    }
}


