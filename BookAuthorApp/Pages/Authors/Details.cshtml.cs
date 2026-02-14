using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookAuthorApp.Pages.Authors
{
    public class DetailsModel : PageModel
    {
        private readonly AuthorService _service;

        public DetailsModel(AuthorService service)
        {
            _service = service;
        }

        public Author Author { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var author = await _service.GetByIdAsync(id);
            if (author == null)
                return RedirectToPage("Index");

            Author = author;
            return Page();
        }
    }
}
