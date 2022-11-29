using System.Security.Claims;
using GdscManagement.Features.Users.Models;
using GdscManagement.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GdscManagement.Shared;

public abstract class BaseComponent<T> : BaseComponent where T : class
{
    private T? _service;

    protected T Service
    {
        get
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            // We cache this because we don't know the lifetime. We have to assume that it could be transient.
            return _service ??= ScopedServices.GetRequiredService<T>();
        }
    }
}

public abstract class BaseComponent : OwningComponentBase
{
    private UserManager<User>? _userManager;
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    protected ClaimsPrincipal? ClaimsPrincipal { get; private set; }
    protected User? User { get; private set; }
    protected bool IsAuthenticated { get; private set; }
    protected bool IsAdmin { get; private set; }

    protected UserManager<User> UserManager
    {
        get
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            // We cache this because we don't know the lifetime. We have to assume that it could be transient.
            return _userManager ??= ScopedServices.GetRequiredService<UserManager<User>>();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        ClaimsPrincipal = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        IsAuthenticated = ClaimsPrincipal.Identity?.IsAuthenticated ?? false;
        IsAdmin = ClaimsPrincipal.IsInRole(Roles.Admin);
        User = await UserManager.GetUserAsync(ClaimsPrincipal);
    }
}
