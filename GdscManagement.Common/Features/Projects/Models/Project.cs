using GdscManagement.Common.Features.Base;

namespace GdscManagement.Common.Features.Projects.Models;


public enum ProjectStatuses{OnGoing, Completed, ToBeDone}
public class Project : Model
{
    public string Title { get; set; }
    
    public string ManagerId { get; set; }
    
    public ProjectStatuses Status { get; set; }  //{Ongoing, Completed, NotStarted}
    
    public string Client { get; set; }

    public string[]  Developers { get; set; }  //user ids
}
    
