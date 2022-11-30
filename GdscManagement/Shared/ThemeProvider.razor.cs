using GdscManagement.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GdscManagement.Shared;

public partial class ThemeProvider : ComponentBase, IDisposable
{
    [Inject] private LayoutService LayoutService { get; set; } = null!;

    private MudThemeProvider _mudThemeProvider = null!;

    protected override void OnInitialized()
    {
        LayoutService.MajorUpdateOccured += LayoutServiceOnMajorUpdateOccured;
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await LayoutService.LoadPreferences();
            // _isDarkMode = await _mudThemeProvider.GetSystemPreference();
            await ApplyUserPreferences();
            StateHasChanged();
        }
    }

    private async Task ApplyUserPreferences()
    {
        var defaultDarkMode = await _mudThemeProvider.GetSystemPreference();
        await LayoutService.ApplyUserPreferences(defaultDarkMode);
    }

    public void Dispose()
    {
        LayoutService.MajorUpdateOccured -= LayoutServiceOnMajorUpdateOccured;
    }

    private void LayoutServiceOnMajorUpdateOccured(object? sender, EventArgs e) => StateHasChanged();
}
