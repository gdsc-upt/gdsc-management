using GdscManagement.Common.Features.Base;
using GdscManagement.Common.Features.Users.Models;

namespace GdscManagement.Common.Features.Workshops.Models;

public class Participants : Model
{
    public Workshop Workshop { get; set; }
    
    public User User { get; set; }
}