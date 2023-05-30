using System.ComponentModel.DataAnnotations;
using GdscManagement.Common.Features.Base;

namespace GdscManagement.Common.Features.Clients.Models;

public class Client : Model
{
    public string Name { get; set; }
    
    [EmailAddress]public string Email { get; set; }
    
    [Phone]public string Phone { get; set; }
}