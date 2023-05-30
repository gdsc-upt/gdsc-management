using System.ComponentModel.DataAnnotations;

namespace GdscManagement.API.Features.Clients.Models;

public class ClientRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [Phone]
    public string Phone { get; set; }
}