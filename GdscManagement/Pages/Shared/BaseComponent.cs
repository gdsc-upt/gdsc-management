using System.Security.Claims;
using AutoMapper;
using GdscManagement.Common.Features.Base;
using GdscManagement.Common.Features.Users;
using GdscManagement.Common.Features.Users.Models;
using GdscManagement.Common.Repository;
using GdscManagement.Features.Base;
using GdscManagement.Utilities.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;

namespace GdscManagement.Pages.Shared;

public abstract class BaseComponent<TModel, TViewModel> : BaseComponent
    where TModel : IModel where TViewModel : IViewModel
{
    [Inject] protected IMapper Mapper { get; set; } = default!;
    protected TModel Map(TViewModel viewModel) => Mapper.Map<TModel>(viewModel);
    protected IEnumerable<TModel> Map(IEnumerable<TViewModel> viewModel) => Mapper.Map<IEnumerable<TModel>>(viewModel);
    protected TViewModel Map(TModel model) => Mapper.Map<TViewModel>(model);
    protected IEnumerable<TViewModel> Map(IEnumerable<TModel> model) => Mapper.Map<IEnumerable<TViewModel>>(model);
}

public abstract class BaseComponent<TService, TModel, TViewModel> : BaseComponent<TService>
    where TService : class, IRepository<IModel> where TModel : IModel
{
    [Inject] protected IMapper Mapper { get; set; } = default!;
    protected TModel Map(TViewModel viewModel) => Mapper.Map<TModel>(viewModel);
    protected IEnumerable<TModel> Map(IEnumerable<TViewModel> viewModel) => Mapper.Map<IEnumerable<TModel>>(viewModel);
    protected TViewModel Map(TModel model) => Mapper.Map<TViewModel>(model);
    protected IEnumerable<TViewModel> Map(IEnumerable<TModel> model) => Mapper.Map<IEnumerable<TViewModel>>(model);
}

public abstract class BaseComponent<TService> : BaseComponent where TService : class, IRepository<IModel>
{
    private TService? _repo;

    protected TService Repo
    {
        get
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            // We cache this because we don't know the lifetime. We have to assume that it could be transient.
            return _repo ??= ScopedServices.GetRequiredService<TService>();
        }
    }
}

public abstract class BaseComponent : OwningComponentBase
{
    [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    [Inject] protected NavigationManager Navigation { get; set; } = default!;
    [Inject] protected IJSRuntime JsRuntime { get; set; } = default!;
    private  SignInManager<User>? _signInManager;

    private UserManager<User>? _userManager;
    protected ClaimsPrincipal? ClaimsPrincipal { get; private set; }
    protected User? User { get; private set; }
    protected bool IsAuthenticated { get; private set; }
    protected bool IsAdmin { get; private set; }

    protected SignInManager<User> SignInManager
    {
        get
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            // We cache this because we don't know the lifetime. We have to assume that it could be transient.
            return _signInManager ??= ScopedServices.GetRequiredService<SignInManager<User>>();
        }
    }

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
        if (user is not null)
        {
            User = user;
        }
        else
        {
            await SignInManager.SignOutAsync();
            // throw new InvalidOperationException("User not found!");
        }
    }

    protected override void OnInitialized()
    {
        Navigation.LocationChanged += TryFragmentNavigation;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await Navigation.NavigateToFragment(JsRuntime);
    }

    private async void TryFragmentNavigation(object? sender, LocationChangedEventArgs args)
    {
        await Navigation.NavigateToFragment(JsRuntime);
    }

    public void Dispose()
    {
        Navigation.LocationChanged -= TryFragmentNavigation;
    }
}
