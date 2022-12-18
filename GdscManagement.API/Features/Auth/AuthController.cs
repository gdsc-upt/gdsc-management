using System.Security.Claims;
using GdscManagement.Common.Features.Users.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GdscManagement.API.Features.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;

    public AuthController(UserManager<User> userManager, TokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Authenticates a Google user and returns a JWT accessToken and refreshToken
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    [AllowAnonymous, HttpPost("authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tokens))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IdentityError))]
    public async Task<Results<Ok<Tokens>, BadRequest<IdentityError>>> Authenticate([FromHeader] string token)
    {
        var payload = await VerifyGoogleTokenId(token);
        if (payload is null)
        {
            var error = new IdentityError { Code = "InvalidToken", Description = "Invalid token" };
            return TypedResults.BadRequest(error);
        }

        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user is null)
        {
            var error = new IdentityError { Code = "UserNotFound", Description = "User not found" };
            return TypedResults.BadRequest(error);
        }

        var claims = new ClaimsIdentity(new Claim[] {
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Name, user.ToString()),
            new(JwtRegisteredClaimNames.Jti, user.Id),
            new(JwtRegisteredClaimNames.Sub, user.Id)
        });

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddClaims(roles.Select(role => new Claim("roles", role)));

        var accessToken = _tokenService.CreateAccessToken(claims);
        return TypedResults.Ok(new Tokens{AccessToken = accessToken, RefreshToken = accessToken});
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
