namespace GdscManagement.API.Features.Auth;

public class Tokens
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
