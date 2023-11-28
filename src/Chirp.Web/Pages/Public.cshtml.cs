﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Chirp.Core;

namespace Chirp.Web.Pages;

[AllowAnonymous]
public class PublicModel : PageModel
{
    private readonly ICheepRepository _service;
    private readonly IAuthorRepository _authorRepo;
    public List<CheepDTO> Cheeps { get; set; }
    public Dictionary<string, bool> IsUserFollowingAuthor { get; set; }
    public int TotalCheeps { get; set; }
    public int PageNumber { get; set; }
    public int CheepsPerPage { get; set; }
    private readonly UserManager<Author> _userManager;


    [BindProperty]
    public NewCheep newCheep { get; set; }

    public PublicModel(ICheepRepository service, IAuthorRepository authorRepo)
    {
        Cheeps = new();
        _service = service;
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

        TotalCheeps = await _service.GetTotalCheepCount();
        Cheeps = await _service.GetCheeps(CheepsPerPage, PageNumber);

        if (User.Identity.IsAuthenticated)
        {
            IsUserFollowingAuthor = new();
            foreach (CheepDTO cheep in Cheeps)
            {
                IsUserFollowingAuthor[cheep.author] = await FindIsUserFollowingAuthor(cheep.author, User.Identity.Name);
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var cheepToPost = new CheepDTO(newCheep.Message, User.Identity.Name, DateTime.UtcNow.ToString());
        await _service.AddCheep(cheepToPost, DateTime.UtcNow);
        return LocalRedirect(Url.Content("~/"));
    }

    public class NewCheep
    {
        public string? Message { get; set; }
    }

    public async Task<bool> FindIsUserFollowingAuthor(string authorUsername, string username)
    {
        return await _authorRepo.IsUserFollowingAuthor(authorUsername, username);
    }

    public async Task<IActionResult> OnPostFollowAuthor(string author)
    {
        await _authorRepo.Follow(User.Identity.Name, author);
        return LocalRedirect(Url.Content("~/"));
    }
}
