namespace GdscManagement.Features.Teams
{
    public readonly struct TeamRoutes
    {
        public const string Main = "/teams";
        public const string Edit = "/teams/{id}";
        public string GetEditRoute(int id) => $"{Main}/{id}";
    }
}