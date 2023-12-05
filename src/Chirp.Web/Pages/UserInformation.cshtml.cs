using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Infrastructure;
using Chirp.Core;

namespace Chirp.Web.Pages;

public class UserInformationModel : PageModel
{
    private readonly UserManager<Author> _userManager;
    private readonly SignInManager<Author> _signInManager;
    private readonly IAuthorRepository _authorRepo;
    public List<AuthorDTO> Following { get; set; }
    public UserInformationModel(UserManager<Author> userManager, SignInManager<Author> signInManager, IAuthorRepository authorRepo)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authorRepo = authorRepo;
    }
    public async Task<ActionResult> OnGet()
    {
        Following = await _authorRepo.GetFollowers(User.Identity.Name);
        return Page();
    }

    public async Task<IActionResult> OnPostForgetUser()
    {
        var user = await _userManager.GetUserAsync(User);
        await _userManager.DeleteAsync(user);
        await _signInManager.SignOutAsync();
        return LocalRedirect(Url.Content("~/"));
    }
}

