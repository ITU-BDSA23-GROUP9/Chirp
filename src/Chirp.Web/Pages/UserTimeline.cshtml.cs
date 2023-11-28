using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace Chirp.Web.Pages;

[AllowAnonymous]
public class UserTimelineModel : PageModel
{
    private readonly ICheepRepository _cheepService;
    private readonly IAuthorRepository _authorService;
    public List<CheepDTO> Cheeps { get; set; }

    public AuthorDTO? author { get; set; }
    public int TotalCheeps { get; set; }
    public int PageNumber { get; set; }
    public int CheepsPerPage { get; set; }
    private readonly UserManager<Author> _userManager;

    [BindProperty]
    public NewCheep newCheep { get; set; }

    public UserTimelineModel(ICheepRepository cheepService, IAuthorRepository authorService, UserManager<Author> userManager)
    {
        Cheeps = new();
        _cheepService = cheepService;
        _authorService = authorService;
        _userManager = userManager;
        PageNumber = 1; // Default to page 1
        CheepsPerPage = 32; // Set the number of cheeps per page
    }

    public async Task<ActionResult> OnGet(string author, int? pageNumber)
    {
        if (pageNumber.HasValue)
        {
            PageNumber = pageNumber.Value;
        }

        TotalCheeps = await _authorService.GetTotalCheepCountFromAuthor(author);
        Cheeps = await _cheepService.GetCheepsFromAuthor(author, CheepsPerPage, PageNumber);
        return Page();
    }

     public async Task<IActionResult> OnPost()
    {
        //var user = await _userManager.GetUserAsync(User);
        //var author = new AuthorDTO(user.UserName, user.Email);
        var cheepToPost = new CheepDTO(newCheep.Message, User.Identity.Name, DateTime.UtcNow.ToString());
        await _cheepService.CreateCheep(cheepToPost);
        return LocalRedirect(Url.Content("~/")); //Go to profile after posting a cheep
    }

    public class NewCheep
    {
        public string? Message { get; set; }
    }
}
