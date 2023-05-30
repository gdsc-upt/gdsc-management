using System.ComponentModel.DataAnnotations;

namespace GdscManagement.API.Features.Projects.Models;

public class DevelopersRequest
{
    [Required] public string ProjectId { get; set; }
    
    [Required] public string UserId { get; set; }
}