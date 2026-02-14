using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookAuthorApp.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly AuthorService _service;

        public IndexModel(AuthorService service)
        {
            _service = service;
        }

        public List<Author> Authors { get; set; } = new();

        public async Task OnGetAsync()
        {
            Authors = await _service.GetAllAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}


