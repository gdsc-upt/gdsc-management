using GdscManagement.Common.Features.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GdscManagement.Areas.Identity.Pages.Account;

public class LogOut : PageModel
{
    public LogOut(SignInManager<User> signInManager)
    {
        SignInManager = signInManager;
    }

    private SignInManager<User> SignInManager { get; set; }

    public async Task<IActionResult> OnGet()
    {
        await SignInManager.SignOutAsync();
        return Redirect("~/");
    }

    public async Task<IActionResult> OnPost()
    {
        await SignInManager.SignOutAsync();
        return Redirect("~/");
    }
}
