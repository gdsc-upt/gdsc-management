using System.Security.Claims;
using GdscManagement.Common.Features.Users.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GdscManagement.API.Features.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly JwtGenerator _jwtGenerator;

    public AuthController(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _jwtGenerator = new JwtGenerator(configuration);
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<ActionResult> Authenticate([FromHeader] string token)
    {
        var payload = await VerifyGoogleTokenId(token);
        if (payload == null)
        {
            return BadRequest("Invalid token");
        }

        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        var claims = new ClaimsIdentity(new Claim[] {
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Name, user.ToString()),
            new(JwtRegisteredClaimNames.Jti, user.Id),
            new(JwtRegisteredClaimNames.Sub, user.Id)
        });

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddClaims(roles.Select(role => new Claim("roles", role)));

        return Ok(_jwtGenerator.CreateAccessToken(claims));
    }

    private static async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleTokenId(string token)
    {
        try
        {
            // uncomment these lines if you want to add settings:
            // var validationSettings = new GoogleJsonWebSignature.ValidationSettings
            // {
            //     Audience = new string[] { "yourServerClientIdFromGoogleConsole.apps.googleusercontent.com" }
            // };
            // Add your settings and then get the payload
            // GoogleJsonWebSignature.Payload payload =  await GoogleJsonWebSignature.ValidateAsync(token, validationSettings);

            // Or Get the payload without settings.
            return await GoogleJsonWebSignature.ValidateAsync(token);
        }
        catch (InvalidJwtException)
        {
            Console.WriteLine("invalid google token");
        }

        return null;
    }
}
