using Blazored.LocalStorage;
using GdscRecruitment.Common.Shared;
using MudBlazor;

namespace GdscManagement.Services;

public class UserPreferences
{
    public bool DarkTheme { get; set; }
}

public class LayoutService
{
    private UserPreferences _userPreferences = new();
    private readonly ILocalStorageService _localStorage;
    private const string Key = "gdsc-mgmt-user-preferences";

    public async Task SaveUserPreferences(UserPreferences userPreferences)
    {
        await _localStorage.SetItemAsync(Key, userPreferences);
    }

    public async Task LoadPreferences()
    {
        var result = await _localStorage.GetItemAsync<UserPreferences>(Key);
        _userPreferences = result ?? new UserPreferences();
    }

    public bool IsDarkMode { get; private set; }

    public MudTheme CurrentTheme { get; set; } = Theme.Get();

    public event EventHandler MajorUpdateOccured;

    private void OnMajorUpdateOccured() => MajorUpdateOccured?.Invoke(this, EventArgs.Empty);


    public LayoutService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task ApplyUserPreferences(bool isDarkModeDefaultTheme)
    {
        IsDarkMode = isDarkModeDefaultTheme;
        _userPreferences.DarkTheme = isDarkModeDefaultTheme;
        await SaveUserPreferences(_userPreferences);
    }


    public async Task ToggleDarkMode()
    {
        IsDarkMode = !IsDarkMode;
        _userPreferences.DarkTheme = IsDarkMode;
        await SaveUserPreferences(_userPreferences);
        OnMajorUpdateOccured();
    }

    public void SetTheme(MudTheme theme)
    {
        CurrentTheme = theme;
    }
    public void SetDarkMode(bool value)
    {
        IsDarkMode = value;
    }
}
