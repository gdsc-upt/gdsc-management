using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Common.Features.Workshops.Models;

namespace GdscManagement.API.Features.Workshops.Models;

public class ParticipantResponse
{
    public Workshop Workshop { get; set; }

    public User User { get; set; }
}