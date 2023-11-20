﻿using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Packaging.Signing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Chirp.Web.Pages;

[AllowAnonymous]
public class PublicModel : PageModel
{
    private readonly ICheepRepository _service;
    private readonly IAuthorRepository _authorRepo; 
    public List<CheepDTO> Cheeps { get; set; }
    public int TotalCheeps { get; set; }
    public int PageNumber { get; set; }
    public int CheepsPerPage { get; set; }
    private readonly UserManager<Author> _userManager;

    
    [BindProperty]
    public NewCheep newCheep { get; set; }

    public PublicModel(ICheepRepository service)
    {
        Cheeps = new();
        _service = service;
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
        return Page();
    }

    public async Task<IActionResult> OnPost() 
    {
        //var user = await _userManager.GetUserAsync(User);
        var author = _authorRepo.FindAuthorByName(User.Identity.Name);
        if (author == null) {
            string userEmail = User.Claims.FirstOrDefault(c => c.Type == "emails")!.Value;
            _authorRepo.CreateAuthor(User.Identity.Name, userEmail);
        }       
        var cheepToPost = new CheepDTO(newCheep.Message, User.Identity.Name, DateTime.UtcNow.ToString());
        await _service.AddCheep(cheepToPost, DateTime.UtcNow);
        return RedirectToPage("/"); //Go to profile after posting a cheep
    }

    public class NewCheep {
        public string? Message {get; set;}
    }
}
