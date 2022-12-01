using System.Security.Claims;
using AutoMapper;
using GdscManagement.Common.Features.Users;
using GdscManagement.Common.Features.Users.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GdscManagement.Shared;

public abstract class BaseComponent<TService, TModel, TViewModel> : BaseComponent<TService> where TService : class
{
    [Inject] protected IMapper Mapper { get; set; } = default!;
    protected TModel Map(TViewModel viewModel) => Mapper.Map<TModel>(viewModel);
    protected IEnumerable<TModel> Map(IEnumerable<TViewModel> viewModel) => Mapper.Map<IEnumerable<TModel>>(viewModel);
    protected TViewModel Map(TModel model) => Mapper.Map<TViewModel>(model);
    protected IEnumerable<TViewModel> Map(IEnumerable<TModel> model) => Mapper.Map<IEnumerable<TViewModel>>(model);
}

public abstract class BaseComponent<TService> : BaseComponent where TService : class
{
    private TService? _service;

    protected TService Service
    {
        get
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            // We cache this because we don't know the lifetime. We have to assume that it could be transient.
            return _service ??= ScopedServices.GetRequiredService<TService>();
        }
    }
}

public abstract class BaseComponent : OwningComponentBase
{
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    private UserManager<User>? _userManager;
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
        var user = await UserManager.GetUserAsync(ClaimsPrincipal);
        User = user ?? throw new InvalidOperationException("User not found!");
    }
}
