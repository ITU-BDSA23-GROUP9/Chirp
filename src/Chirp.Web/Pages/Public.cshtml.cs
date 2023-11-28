using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Packaging.Signing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Duende.IdentityServer.Extensions;
using System.ComponentModel.DataAnnotations;

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

    public PublicModel(ICheepRepository service, UserManager<Author> userManager)
    {
        Cheeps = new();
        _service = service;
        _userManager = userManager;
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
        //var author = new AuthorDTO(user.UserName, user.Email);
        var cheepToPost = new CheepDTO(newCheep.Message, User.Identity.Name, DateTime.UtcNow.ToString());
        await _service.CreateCheep(cheepToPost);
        return LocalRedirect(Url.Content("~/")); //Go to profile after posting a cheep
    }

    public class NewCheep
    {
        [Required, StringLength(160)]
        public string? Message { get; set; }
    }
}
