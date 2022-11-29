using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace GdscManagement.Utilities;

public readonly struct CustomClaimTypes
{
    public const string Picture = "urn:google:picture";
    public const string EmailVerified = "urn:google:verified";
}

public class CustomClaimAction : ClaimAction
{
    public CustomClaimAction(string claimType, string valueType) : base(claimType, valueType)
    {
    }

    public override void Run(JsonElement userData, ClaimsIdentity identity, string issuer)
    {
        foreach (var jsonProperty in userData.EnumerateObject())
            Console.WriteLine(jsonProperty.Name + ": " + jsonProperty.Value);
    }
}
