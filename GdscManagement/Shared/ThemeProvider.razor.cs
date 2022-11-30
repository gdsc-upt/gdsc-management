using GdscManagement.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GdscManagement.Shared;

public partial class ThemeProvider : ComponentBase, IDisposable
{
    [Inject] private PreferencesService PreferencesService { get; set; } = null!;

    protected override void OnInitialized()
    {
        PreferencesService.MajorUpdateOccured += PreferencesServiceOnMajorUpdateOccured;
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await PreferencesService.Load();
            StateHasChanged();
        }
    }

    public void Dispose()
    {
        PreferencesService.MajorUpdateOccured -= PreferencesServiceOnMajorUpdateOccured;
    }

    private void PreferencesServiceOnMajorUpdateOccured(object? sender, EventArgs e) => StateHasChanged();
}
