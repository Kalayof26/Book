using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookAuthorApp.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly BookService _service;
        public DeleteModel(BookService service) => _service = service;

        [BindProperty]
        public Book Book { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var book = await _service.GetByIdAsync(id);
            if (book == null) return RedirectToPage("Index");
            Book = book;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToPage("Index");
        }
    }
}
