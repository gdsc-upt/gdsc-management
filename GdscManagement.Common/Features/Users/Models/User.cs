using GdscManagement.Common.Features.Base;
using Microsoft.AspNetCore.Identity;

namespace GdscManagement.Common.Features.Users.Models;

public class User : IdentityUser, IModel
{
    public User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Created = Updated = DateTime.UtcNow;
    }

    [PersonalData] public string FirstName { get; set; }
    [PersonalData] public string LastName { get; set; }
    [PersonalData] public string? Avatar { get; set; }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
