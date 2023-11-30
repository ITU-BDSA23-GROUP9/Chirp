using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class UserInformationModel : PageModel
    {
        private readonly UserManager<Author> _userManager;
        private readonly SignInManager<Author> _signInManager;
        public UserInformationModel(UserManager<Author> userManager, SignInManager<Author> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostForgetUser()
        {
            var user = await _userManager.GetUserAsync(User);
            await _userManager.DeleteAsync(user);
            await _signInManager.SignOutAsync();
            return LocalRedirect(Url.Content("~/"));
        }
    }
}
