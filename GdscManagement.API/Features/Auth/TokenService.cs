using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GdscManagement.API.Features.Auth;

public class TokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _key = new SymmetricSecurityKey(Convert.FromBase64String(GetSigningKey()));
    }

    private string GetSigningKey()
    {
        var signingKeys = _configuration.GetSection(Jwt.SigningKeys).GetChildren();
        var signingKey = signingKeys.SingleOrDefault(key => key[Jwt.Issuer] == _configuration.GetSection(Jwt.ValidIssuers).GetChildren().ToList()[0].Value);
        return signingKey?[Jwt.Value] ?? throw new AuthenticationException("No signing key found");
    }

    public string CreateAccessToken(ClaimsIdentity claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration.GetSection(Jwt.ValidAudiences).GetChildren().ToList()[0].Value,
            Issuer = _configuration.GetSection(Jwt.ValidIssuers).GetChildren().ToList()[0].Value,
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string CreateRefreshToken()
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration.GetSection(Jwt.ValidAudiences).GetChildren().ToList()[0].Value,
            Issuer = _configuration.GetSection(Jwt.ValidIssuers).GetChildren().ToList()[0].Value,
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
