namespace GdscManagement.API.Features.Projects.Models;

public class ProjectResponse
{
    public string Title { get; set; }
    
    public string ManagerId { get; set; }
    
    public string Status { get; set; }
    
    public string Client { get; set; }
    
    public string[] Developers { get; set; }
}