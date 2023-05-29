using System.ComponentModel.DataAnnotations;
using GdscManagement.Common.Features.Projects.Models;
using GdscManagement.Common.Features.Users.Models;

namespace GdscManagement.API.Features.Projects.Models;

public class ProjectRequest
{
    [Required]  public string Title { get; set; }
    
    [Required]  public  string ManagerId { get; set; }
    
    [Required]  public ProjectStatuses  Status { get; set; }
    
    [Required]  public string Client { get; set; }

    


}