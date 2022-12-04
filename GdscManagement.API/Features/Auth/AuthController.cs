using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GdscManagement.API.Features.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("/verify")]
    public async Task<ActionResult> Verify([FromHeader] string token)
    {
        token = token.Remove(0, 7); //remove Bearer
        var payload = await VerifyGoogleTokenId(token);
        if (payload == null)
        {
            return BadRequest("Invalid token");
        }


        return Ok(payload);
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
        catch (Exception)
        {
            Console.WriteLine("invalid google token");
        }

        return null;
    }
}
