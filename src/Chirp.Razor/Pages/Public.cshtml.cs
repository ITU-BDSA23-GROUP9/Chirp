using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepRepository _service;
    public List<CheepDTO> Cheeps { get; set; }

    public PublicModel(ICheepRepository service)
    {
        _service = service;
    }

    public async Task<ActionResult> OnGet()
    {
        Cheeps = await _service.GetCheeps();
        return Page();
    }
}
