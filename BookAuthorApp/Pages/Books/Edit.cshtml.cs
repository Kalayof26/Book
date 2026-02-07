using BookAuthorApp.Models;
using BookAuthorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookAuthorApp.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;

        public EditModel(BookService bookService, AuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        [BindProperty]
        public Book Book { get; set; } = new();

        public SelectList AuthorsSelect { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var existing = await _bookService.GetByIdAsync(id);
            if (existing == null)
                return RedirectToPage("Index");

            Book = existing;

            var authors = await _authorService.GetAllAsync();
            AuthorsSelect = new SelectList(authors, "Id", "FirstName", Book.AuthorId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var authors = await _authorService.GetAllAsync();
                AuthorsSelect = new SelectList(authors, "Id", "FirstName", Book.AuthorId);
                return Page();
            }

            await _bookService.UpdateAsync(Book);
            return RedirectToPage("Index");
        }
    }
}
