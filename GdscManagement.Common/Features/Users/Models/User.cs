using GdscManagement.Common.Features.Base;
using Microsoft.AspNetCore.Identity;

namespace GdscManagement.Common.Features.Users.Models;

public class User : IdentityUser, IModel
{
    public User()
    {
        Created = Updated = DateTime.UtcNow;
    }

    [PersonalData] public virtual string? FirstName { get; set; }
    [PersonalData] public virtual string? LastName { get; set; }
    [PersonalData] public virtual string? Avatar { get; set; }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}