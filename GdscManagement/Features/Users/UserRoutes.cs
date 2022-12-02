namespace GdscManagement.Features.Users;

public readonly struct UserRoutes
{
    public const string Main = "/users";
    public const string Edit = "/users/{id}";

    public string GetEditRoute(int id) => $"{Main}/{id}";
}
