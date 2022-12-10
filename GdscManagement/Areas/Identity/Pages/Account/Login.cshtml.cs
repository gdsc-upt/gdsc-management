using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GdscManagement.Areas.Identity.Pages.Account;

public class Login : PageModel
{
    public async Task<IActionResult> OnGet(string returnUrl = "/")
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return Redirect("~/");
        }

        await HttpContext.ChallengeAsync("Google", new AuthenticationProperties
        {
            RedirectUri = Url.Page("./ExternalLogin", "Callback", new { returnUrl })
        });

        return new EmptyResult();
    }
}
