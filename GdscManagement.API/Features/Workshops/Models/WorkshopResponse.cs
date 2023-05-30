using GdscManagement.API.Features.Users.Models;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Common.Features.Workshops.Models;

namespace GdscManagement.API.Features.Workshops.Models;

public class WorkshopResponse
{
    public User Trainer { get; set; }
    
    public string Topic { get; set; }
    
    public string Description { get; set; }
    
    public DateTime DateStart { get; set; }
    
    public DateTime DateEnd { get; set; }
    
    public int MaxCapacity { get; set; }
    
    public int OccupiedSeates { get; set; }
    
    public string Location { get; set; }
    
    public List<UserResponse> Participants { get; set; }
    
    public string Presentation { get; set; }
}