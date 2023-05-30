using System.ComponentModel.DataAnnotations;

namespace GdscManagement.API.Features.Workshops.Models;

public class ParticipantRequest
{
    [Required] public string WorkshopId { get; set; }
    
    [Required] public string UserId { get; set; }
}