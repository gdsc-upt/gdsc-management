using System.Text.Json.Serialization;
using GdscManagement.Common.Features.Base;
using GdscManagement.Common.Features.Users.Models;

namespace GdscManagement.Common.Features.Workshops.Models;

public class Workshop : Model
{
    

    public User Trainer { get; set; }
    
    public string Topic { get; set; }
    
    public string Description { get; set; }
    
    public DateTime DateStart { get; set; }
    
    public DateTime DateEnd { get; set; }
    
    public int MaxCapacity { get; set; }
    
    public int OccupiedSeates { get; set; }
    
    public string Location { get; set; }
    
    [JsonIgnore]public List<User> Participants { get; set; }

    public string Presentation { get; set; }
}