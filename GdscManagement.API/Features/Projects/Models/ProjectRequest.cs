using System.ComponentModel.DataAnnotations;

namespace GdscManagement.API.Features.Projects.Models;

public class ProjectRequest
{
    [Required]  public string Title { get; set; }
    
    [Required]  public string ManagerId { get; set; }
    
    [Required]  public string Status { get; set; }
    
    [Required]  public string Client { get; set; }
    
    [Required]  public string[] Developers { get; set; }
    
    
}