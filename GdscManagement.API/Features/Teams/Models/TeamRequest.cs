using System.ComponentModel.DataAnnotations;

namespace GdscManagement.API.Features.Teams.Models;

public class TeamRequest
{
    [Required]public string TeamLeadId { get; set; }
    
    [Required]public string Name { get; set; }
    
    [Required]public int MembersCount { get; set; }
}