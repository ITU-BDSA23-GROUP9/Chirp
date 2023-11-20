using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages
{
    public class CheepModel : PageModel
    {
        private readonly ICheepRepository _repository; 

        [BindProperty]
        public Cheep cheep { get; set; }
        public void OnGet()
        {
        }

        /*
        public async Task<IActionResult> OnPost() {
            var cheepToPost = new CheepDTO(cheep.Text, cheep.Author.ToString(), cheep.TimeStamp.ToString())

            await _repository.CreateCheep(cheepToPost);

            return RedirectToPage("/Profile"); //Go to profile after posting a cheep
        }
        */
    }
}
