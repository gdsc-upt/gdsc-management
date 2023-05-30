using GdscManagement.Common.Features.Projects.Models;
using GdscManagement.Common.Features.Teams.Models;
using GdscManagement.Common.Features.Users;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Common.Features.Workshops.Models;
using GdscManagement.Common.Features.UsersProfile.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GdscManagement.Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}
    
    public DbSet<Team> Teams { get; set; }
    
    public DbSet<Project> Projects { get; set; }
    
    public DbSet <Developers> Developers { get; set; }
    
    public DbSet<Participants> Participants { get; set; }

    public DbSet<Workshop> Workshops { get; set; }

    public DbSet<UserProfile> AspNetUserProfile { get; set; }
}
