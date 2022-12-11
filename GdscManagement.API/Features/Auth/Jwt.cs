namespace GdscManagement.API.Features.Auth;

public readonly struct Jwt
{
    private const string BearerScheme = "Authentication:Schemes:Bearer";
    public const string ValidIssuers = $"{BearerScheme}:ValidIssuers";
    public const string ValidAudiences = $"{BearerScheme}:ValidAudiences";
    public const string SigningKeys = $"{BearerScheme}:SigningKeys";
    public const string Issuer = "Issuer";
    public const string Value = "Value";
}
