using GdscManagement.Common.Features.Users.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model = GdscManagement.Common.Features.Base.Model;

namespace GdscManagement.Common.Features.Projects.Models;

public class Developers : Model
{
     public Project Project { get; set; }
     
     public User User { get; set; }
}