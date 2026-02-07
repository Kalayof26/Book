using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookAuthorApp.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly AuthorService _service;
        public DeleteModel(AuthorService service) => _service = service;

        [BindProperty]
        public Author Author { get; set; } = new();

        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var author = await _service.GetByIdAsync(id);
            if (author == null) return RedirectToPage("Index");
            Author = author;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                var author = await _service.GetByIdAsync(id);
                if (author != null) Author = author;
                ErrorMessage = "Cannot delete author with books!";
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}
