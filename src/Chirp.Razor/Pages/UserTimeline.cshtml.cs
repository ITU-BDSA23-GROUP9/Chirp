using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepRepository _service;
    public List<CheepDTO>? Cheeps { get; set; }

    public Author? author { get; set; }
    public int TotalCheeps { get; set; }
    public int PageNumber { get; set; }
    public int CheepsPerPage { get; set; }

    public UserTimelineModel(ICheepRepository service)
    {
        _service = service;
        PageNumber = 1; // Default to page 1
        CheepsPerPage = 32; // Set the number of cheeps per page
    }

    public async Task<ActionResult> OnGet(string author, int? pageNumber)
    {
        if (pageNumber.HasValue)
        {
            PageNumber = pageNumber.Value;
        }

        TotalCheeps = await _service.GetTotalCheepCountFromAuthor(author);
        Cheeps = await _service.GetCheepsFromAuthor(author, CheepsPerPage, PageNumber);
        return Page();
    }
}
