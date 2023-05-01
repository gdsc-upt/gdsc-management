using System.ComponentModel.DataAnnotations;

namespace GdscManagement.API.Features.Clients.Models;

public class ClientResponse
{
    public string Name { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    public string Phone { get; set; }
}