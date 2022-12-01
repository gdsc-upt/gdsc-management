using System.Security.Claims;
using Blazored.LocalStorage;
using GdscManagement.API;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using GdscManagement.Areas.Identity;
using GdscManagement.Common;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Data;
using GdscManagement.Services;
using GdscManagement.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
var connectionString = configuration.GetConnectionString("Default") ??
                       throw new InvalidOperationException("Connection string 'Default' not found.");

// Add services to the container.
services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
services.AddDatabaseDeveloperPageExceptionFilter();

var identityBuilder = services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true);
identityBuilder.AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

services.AddRazorPages();
services.AddServerSideBlazor();
services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
services.AddBlazoredLocalStorage();
services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
services.AddScoped(typeof(ViewModelHelper<>));
services.AddScoped<PreferencesService>();

services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = configuration["Google:ClientId"] ??
                       throw new InvalidOperationException("Google CLient ID not found.");
    options.ClientSecret = configuration["Google:ClientSecret"] ??
                           throw new InvalidOperationException("Google Client Secret not found.");
    options.ClaimActions.MapJsonKey(CustomClaimTypes.Picture, "picture", "url");
    options.ClaimActions.MapJsonKey(CustomClaimTypes.EmailVerified, "verified_email", "bool");
    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name", "string");
    options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name", "string");
    options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name", "string");
});

services.AddCommon<ApplicationDbContext>();
services.AddApi();
services.AddAutoMapper(typeof(AdminMappingProfiles));

var app = builder.Build();

app.MigrateIfNeeded();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseApi();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
