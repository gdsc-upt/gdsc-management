using System.ComponentModel.DataAnnotations;

namespace GdscManagement.API.Features.Workshops.Models;

public class WorkshopRequest
{
    [Required]public string TrainerId { get; set; }
    
    [Required]public string Topic { get; set; }
    
    [Required]public string Description { get; set; }
    
    [Required]public DateTime DateStart { get; set; }
    
    [Required]public DateTime DateEnd { get; set; }
    
    [Required]public int MaxCapacity { get; set; }

    [Required]public string Location { get; set; }

    [Required]public string Presentation { get; set; }
}