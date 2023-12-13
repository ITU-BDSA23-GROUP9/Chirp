using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Chirp.Core;
using Chirp.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Duende.IdentityServer.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Chirp.Web.Pages;

[AllowAnonymous]
public class PublicModel : PageModel
{
    private readonly ICheepRepository _cheepRepo;
    private readonly IAuthorRepository _authorRepo;
    public List<CheepDTO> Cheeps { get; set; }
    public Dictionary<string, bool>? IsUserFollowingAuthor { get; set; }
    public int TotalCheeps { get; set; }
    public int PageNumber { get; set; }
    public int CheepsPerPage { get; set; }


    [BindProperty]
    public NewCheep? newCheep { get; set; }

    public PublicModel(ICheepRepository cheepRepo, IAuthorRepository authorRepo)
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

        TotalCheeps = await _cheepRepo.GetTotalCheepCount();
        Cheeps = await _cheepRepo.GetCheeps(CheepsPerPage, PageNumber);

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
        var cheepToPost = new CheepDTO(Guid.NewGuid().ToString(), newCheep?.Message!, User.Identity?.Name!, DateTime.UtcNow.ToString());
        await _cheepRepo.CreateCheep(cheepToPost);
        return LocalRedirect(Url.Content("~/")); //Go to profile after posting a cheep
    }

    public class NewCheep
    {
        [Required, StringLength(160)]
        public string? Message { get; set; }
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

    public async Task<IActionResult> OnPostLikeCheep(string cheepId)
    {
        await _cheepRepo.Like(cheepId, User.Identity?.Name!);
        return LocalRedirect(Url.Content("~/"));
    }

    public async Task<IActionResult> OnPostDislikeCheep(string cheepId)
    {
        await _cheepRepo.Dislike(cheepId, User.Identity?.Name!);
        return LocalRedirect(Url.Content("~/"));
    }

    public async Task<int> GetCheepLikesCount(string cheepId)
    {
        return await _cheepRepo.GetLikesCount(cheepId);
    }

    public async Task<bool> HasUserLikedCheep(string cheepId)
    {
        return await _cheepRepo.HasUserLikedCheep(cheepId, User.Identity?.Name!);
    }

}
