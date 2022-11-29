using Microsoft.AspNetCore.Identity;

namespace GdscManagement.Features.Users.Models;

public class User : IdentityUser
{
    public User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    [PersonalData] public string FirstName { get; set; }
    [PersonalData] public string LastName { get; set; }
    [PersonalData] public string? Avatar { get; set; }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}
