using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Infrastructure;
using Chirp.Core;

namespace Chirp.Web.Pages;

public class PrivateTimelineModel : PageModel
{

    private readonly ICheepRepository _cheepRepo;
    private readonly IAuthorRepository _authorRepo;
    public List<CheepDTO> Cheeps { get; set; }

    public AuthorDTO? author { get; set; }
    public int TotalCheeps { get; set; }
    public int PageNumber { get; set; }
    public int CheepsPerPage { get; set; }

    [BindProperty]
    public NewCheep newCheep { get; set; }
    private readonly UserManager<Author> _userManager;
    private readonly SignInManager<Author> _signInManager;
    public PrivateTimelineModel(ICheepRepository cheepRepo, IAuthorRepository authorRepo)
    {
        Cheeps = new();
        _cheepRepo = cheepRepo;
        _authorRepo = authorRepo;
        PageNumber = 1; // Default to page 1
        CheepsPerPage = 32; // Set the number of cheeps per page
    }

    public async Task<ActionResult> OnGet(int? pageNumber)
    {
        if (pageNumber.HasValue)
        {
            PageNumber = pageNumber.Value;
        }

        //TotalCheeps = await _authorRepo.GetTotalCheepCountFromAuthor(author);
        Cheeps = await _cheepRepo.GetCheeps(CheepsPerPage, PageNumber);
         return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        //var user = await _userManager.GetUserAsync(User);
        //var author = new AuthorDTO(user.UserName, user.Email);
        var cheepToPost = new CheepDTO(newCheep.Message, User.Identity.Name, DateTime.UtcNow.ToString());
        await _cheepRepo.CreateCheep(cheepToPost);
        return LocalRedirect(Url.Content("~/"));
    }
    public class NewCheep
    {
        public string? Message { get; set; }
    }
}

