using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GdscManagement.Utilities.Extensions;

public static class NavigationManagerExtensions
{
    public static ValueTask NavigateToFragment(this NavigationManager navigationManager, IJSRuntime jSRuntime)
    {
        var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
        var hasFragment = uri.Fragment.Length > 0;

        return hasFragment ? jSRuntime.InvokeVoidAsync("blazorHelpers.scrollToFragment", uri.Fragment[1..]) : default;
    }
}
