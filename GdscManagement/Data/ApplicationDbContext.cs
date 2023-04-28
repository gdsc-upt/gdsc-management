using GdscManagement.Common.Features.Projects.Models;
using GdscManagement.Common.Features.Teams.Models;
using GdscManagement.Common.Features.Users;
using GdscManagement.Common.Features.Users.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}
    
    public DbSet<Team> Teams { get; set; }
    
    public DbSet<Project> Projects { get; set; }
}
