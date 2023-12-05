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
    public Dictionary<string, bool>? IsUserFollowingAuthor { get; set; }
    public int TotalCheeps { get; set; }
    public int PageNumber { get; set; }
    public int CheepsPerPage { get; set; }

    [BindProperty]
    public NewCheep? newCheep { get; set; }

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
        string username = User.Identity?.Name!;
        if (pageNumber.HasValue)
        {
            PageNumber = pageNumber.Value;
        }
        TotalCheeps = await _authorRepo.GetTotalCheepCountFromFollowersAndAuthor(username);
        Cheeps = await _cheepRepo.GetPrivateTimelineCheeps(username, CheepsPerPage, PageNumber);

        if (User.Identity?.IsAuthenticated == true)
        {
            IsUserFollowingAuthor = new();
            foreach (CheepDTO cheep in Cheeps)
            {
                IsUserFollowingAuthor[cheep.author] = await FindIsUserFollowingAuthor(cheep.author, User.Identity?.Name!);
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        //var user = await _userManager.GetUserAsync(User);
        //var author = new AuthorDTO(user.UserName, user.Email);
        var cheepToPost = new CheepDTO(newCheep!.Message!, User.Identity?.Name!, DateTime.UtcNow.ToString());
        await _cheepRepo.CreateCheep(cheepToPost);
        return LocalRedirect(Url.Content("~/"));
    }

    public async Task<bool> FindIsUserFollowingAuthor(string authorUsername, string username)
    {
        return await _authorRepo.IsUserFollowingAuthor(authorUsername, username);
    }

    public async Task<IActionResult> OnPostFollowAuthor(string author)
    {
        await _authorRepo.Follow(User.Identity?.Name!, author);
        return LocalRedirect(Url.Content("~/"));
    }

    public async Task<IActionResult> OnPostUnfollowAuthor(string author)
    {
        await _authorRepo.Unfollow(User.Identity?.Name!, author);
        return LocalRedirect(Url.Content("~/"));
    }

    public class NewCheep
    {
        public string? Message { get; set; }
    }
}

