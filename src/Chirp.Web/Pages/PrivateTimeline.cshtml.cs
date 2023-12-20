using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core;

namespace Chirp.Web.Pages;

/// <summary>
/// Razor page arepresenting the private timeline of an author.
/// </summary>
public class PrivateTimelineModel : PageModel
{

    private readonly ICheepRepository _cheepRepo;
    private readonly IAuthorRepository _authorRepo;

    /// <summary>
    /// Gets or sets the cheeps displayed on a private timeline.
    /// </summary>
    public List<CheepDTO> Cheeps { get; set; }
    /// <summary>
    /// Gets or sets the author associated with a private timeline.
    /// </summary>
    public AuthorDTO? author { get; set; }

    /// <summary>
    /// Gets or sets a dictionary saying whether a user is following an author or not.
    /// </summary>
    public Dictionary<string, bool>? IsUserFollowingAuthor { get; set; }

    /// <summary>
    /// Gets or sets the total cheeps. 
    /// </summary>
    public int TotalCheeps { get; set; }

    /// <summary>
    /// Gets or sets the current page number.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the number of cheeps allowed per page.
    /// </summary>
    public int CheepsPerPage { get; set; }

    /// <summary>
    /// Gets or sets the redirectionURL.
    /// </summary>
    public string? RedirectUrl { get; set; }

    /// <summary>
    /// Gets or sets a new cheep.
    /// </summary>
    [BindProperty]
    public string? NewCheep { get; set; }

    /// <summary>
    /// Initializes a new instance of private timeline.
    /// </summary>
    /// <param name="cheepRepo"></param>
    /// <param name="authorRepo"></param>
    public PrivateTimelineModel(ICheepRepository cheepRepo, IAuthorRepository authorRepo)
    {
        Cheeps = new();
        _cheepRepo = cheepRepo;
        _authorRepo = authorRepo;
        PageNumber = 1;
        CheepsPerPage = 32;
    }

    /// <summary>
    /// Handles HTTP GET requests for the private timeline.
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <returns>The IActionResult representing the result of the operation.</returns>
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
        RedirectUrl ??= Url.Content("/private");

        return Page();
    }

    /// <summary>
    /// Handles HTTP POST requests for the private timeline.
    /// </summary>
    /// <returns>The IActionResult representing the result of the operation.</returns>
    public async Task<IActionResult> OnPost()
    {
        if (NewCheep == null)
        {
            return LocalRedirect(Url.Content("/private"));
        }
        var cheepToPost = new CheepDTO(Guid.NewGuid().ToString(), NewCheep, User.Identity?.Name!, DateTime.UtcNow.ToString());
        
        await _cheepRepo.CreateCheep(cheepToPost);
        return RedirectToPage(RedirectUrl);
    }

    public async Task<bool> FindIsUserFollowingAuthor(string authorUsername, string username)
    {
        return await _authorRepo.IsUserFollowingAuthor(authorUsername, username);
    }

    public async Task<IActionResult> OnPostFollowAuthor(string author)
    {
        await _authorRepo.Follow(User.Identity?.Name!, author);
        return RedirectToPage(RedirectUrl);
    }

    public async Task<IActionResult> OnPostUnfollowAuthor(string author)
    {
        await _authorRepo.Unfollow(User.Identity?.Name!, author);
        return RedirectToPage(RedirectUrl);
    }

    public async Task<IActionResult> OnPostLikeCheep(string cheepId)
    {
        await _cheepRepo.Like(cheepId, User.Identity?.Name!);
        return RedirectToPage(RedirectUrl);
    }

    public async Task<IActionResult> OnPostDislikeCheep(string cheepId)
    {
        await _cheepRepo.Dislike(cheepId, User.Identity?.Name!);
        return RedirectToPage(RedirectUrl);
    }

    public async Task<int> GetCheepLikesCount(string cheepId)
    {
        return await _cheepRepo.GetLikesCount(cheepId);
    }

    public async Task<bool> HasUserLikedCheep(string cheepId)
    {
        return await _cheepRepo.HasUserLikedCheep(cheepId, User.Identity?.Name!);
    }

    /// <summary>
    /// Formats the timestamp to remove unnecessary trailing zeros.
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns>The formatted timestamp</returns>
    public string FormatTimestamp(string timestamp)
    {
        if (timestamp.EndsWith(".0000000"))
        {
            return timestamp.Substring(0, timestamp.Length - 8);
        }
        return timestamp;
    }
}

