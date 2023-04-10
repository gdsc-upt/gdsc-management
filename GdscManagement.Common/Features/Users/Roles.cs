using GdscManagement.Common.Features.Base;
using Microsoft.AspNetCore.Identity;

namespace GdscManagement.Common.Features.Users;

public readonly struct Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
}


public class Role : IdentityRole, IModel
{   public Role(string roleName) : base(roleName)
    {
      
    }
    
    public Role () : base()
    {
      
    }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}