using ChurrasTrinca.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChurrasTrinca.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public LoginViewModel Model { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(Model.Email, Model.Password, Model.RememberMe, false);

                if (identityResult.Succeeded)
                {
                    if (returnUrl == null || returnUrl == "/")
                    {
                        return Redirect("~/Index");
                    }
                    else
                    {
                        return RedirectToPage(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-mail ou senha estão incorretos!");
                }
            }

            return Page();
        }
    }
}
