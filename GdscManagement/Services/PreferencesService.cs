using Blazored.LocalStorage;
using GdscManagement.Pages.Shared;
using MudBlazor;

namespace GdscManagement.Services;

public class UserPreferences
{
    public bool IsDarkMode { get; set; }
}

// TODO: Add multiple themes and a theme selector
public class PreferencesService
{
    private UserPreferences _preferences = new();
    private readonly ILocalStorageService _localStorage;
    private const string Key = "gdsc-mgmt-user-preferences";

    private async Task Save(UserPreferences userPreferences)
    {
        await _localStorage.SetItemAsync(Key, userPreferences);
    }

    public async Task Load()
    {
        var result = await _localStorage.GetItemAsync<UserPreferences>(Key);
        _preferences = result ?? new UserPreferences();
    }

    public bool IsDarkMode => _preferences.IsDarkMode;

    public MudTheme CurrentTheme { get; set; } = Theme.Get();

    public event EventHandler MajorUpdateOccured = null!;

    private void OnMajorUpdateOccured() => MajorUpdateOccured.Invoke(this, EventArgs.Empty);


    public PreferencesService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task Apply(bool isDarkModeDefaultTheme)
    {
        _preferences.IsDarkMode = isDarkModeDefaultTheme;
        await Save(_preferences);
    }


    public async Task ToggleDarkMode()
    {
        _preferences.IsDarkMode = !_preferences.IsDarkMode;
        await Save(_preferences);
        OnMajorUpdateOccured();
    }

    public void SetTheme(MudTheme theme)
    {
        CurrentTheme = theme;
    }
    public void SetDarkMode(bool value)
    {
        _preferences.IsDarkMode = value;
    }
}
